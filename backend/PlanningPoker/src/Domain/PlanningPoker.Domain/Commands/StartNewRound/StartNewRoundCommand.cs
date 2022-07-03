using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.GameQueries;

namespace PlanningPoker.Domain.Commands.StartNewRound
{
    public class StartNewRoundCommand : Notifiable, IRequest<StartNewRoundCommandResponseDTO>
    {
        public StartNewRoundCommand(Guid gameId, string roundName)
        {
            GameId = gameId;
            RoundName = roundName;
        }

        public Guid GameId { get; private set; }
        public string RoundName { get; private set; }

        public override async Task SubscribeRulesAsync(IMediator mediator, CancellationToken cancellationToken = default)
        {
            var gameIdValidationResult = await GameIdIsValidAsync(mediator, cancellationToken);

            AddNotifications(new Contract<StartNewRoundCommand>()
                .IsTrue(gameIdValidationResult.IsValid, nameof(GameId), gameIdValidationResult.Msg));
        }

        private async Task<(bool IsValid, string Msg)> GameIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (GameId == Guid.Empty)
                return (false, "Game id is required");
            if (!await mediator.Send(new GetExistsGameByIdQuery(GameId), cancellationToken))
                return (false, "There is no game with the given id");

            return (true, "");
        }
    }
}
