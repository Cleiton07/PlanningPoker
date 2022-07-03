using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.GameQueries;

namespace PlanningPoker.Domain.Commands.AddPlay
{
    public class AddPlayCommand : Notifiable, IRequest
    {
        public AddPlayCommand(Guid gameId, Guid deckItemId, Guid playerId)
        {
            GameId = gameId;
            DeckItemId = deckItemId;
            PlayerId = playerId;
        }

        public Guid GameId { get; private set; }
        public Guid DeckItemId { get; private set; }
        public Guid PlayerId { get; private set; }

        public override async Task SubscribeRulesAsync(IMediator mediator, CancellationToken cancellationToken = default)
        {
            var gameIdValidationResult = await GameIdIsValidAsync(mediator, cancellationToken);
            var deckItemIdValidationResult = await DeckItemIdIsValidAsync(mediator, cancellationToken);
            var playerIdValidationResult = await PlayerIdIsValidAsync(mediator, cancellationToken);

            AddNotifications(new Contract<AddPlayCommand>()
                .IsTrue(gameIdValidationResult.IsValid, nameof(GameId), gameIdValidationResult.Msg)
                .IsTrue(deckItemIdValidationResult.IsValid, nameof(GameId), deckItemIdValidationResult.Msg)
                .IsTrue(playerIdValidationResult.IsValid, nameof(GameId), playerIdValidationResult.Msg));
        }

        private async Task<(bool IsValid, string Msg)> GameIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (GameId == Guid.Empty)
                return (false, "Game id is required");
            if (!await mediator.Send(new GetExistsGameByIdQuery(GameId), cancellationToken))
                return (false, "Game id provided not exists");
            if (!await mediator.Send(new GetExistsActiveRoundByGameIdQuery(GameId), cancellationToken))
                return (false, "This game does not have an active round");

            return (true, "");
        }

        private async Task<(bool IsValid, string Msg)> DeckItemIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (DeckItemId == Guid.Empty)
                return (false, "Deck id is required");
            if (!await mediator.Send(new GetExistsDeckItemInTheGameQuery(DeckItemId, GameId), cancellationToken))
                return (false, "The game does not contain a deck item with the given id");

            return (true, "");
        }

        private async Task<(bool IsValid, string Msg)> PlayerIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (PlayerId == Guid.Empty)
                return (false, "Player id is required");
            if (!await mediator.Send(new GetExistsPlayerInTheGameQuery(PlayerId, GameId), cancellationToken))
                return (false, "The game does not contain a player with the given id");

            return (true, "");
        }
    }
}
