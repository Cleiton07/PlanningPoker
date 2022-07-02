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
            AddNotifications(new Contract<StartNewRoundCommand>()
                .IsTrue(await GameIdIsValidAsync(mediator, cancellationToken), nameof(GameId), "Invalid game id"));
        }

        private async Task<bool> GameIdIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if(GameId == Guid.Empty) return false;
            return await mediator.Send(new GetExistsGameByIdQuery(GameId), cancellationToken);
        }
    }
}
