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
            AddNotifications(new Contract<AddPlayCommand>()
                .IsTrue(await GameIdIsValidAsync(mediator, cancellationToken), nameof(GameId), "Invalid game id")
                .IsTrue(await DeckItemIdIsValidAsync(mediator, cancellationToken), nameof(GameId), "Invalid deck item id")
                .IsTrue(await PlayerIdIsValidAsync(mediator, cancellationToken), nameof(GameId), "Invalid player id"));
        }

        private async Task<bool> GameIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (GameId == Guid.Empty) return false;
            return await mediator.Send(new GetExistsActiveRoundByGameIdQuery(GameId), cancellationToken);
        }

        private async Task<bool> DeckItemIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (DeckItemId == Guid.Empty) return false;
            return await mediator.Send(new GetExistsDeckItemInTheGameQuery(DeckItemId, GameId), cancellationToken);
        }

        private async Task<bool> PlayerIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (PlayerId == Guid.Empty) return false;
            return await mediator.Send(new GetExistsPlayerInTheGameQuery(PlayerId, GameId), cancellationToken);
        }
    }
}
