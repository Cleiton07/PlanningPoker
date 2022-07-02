using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.GameQueries;

namespace PlanningPoker.Domain.Commands.AddPlayerInTheGame
{
    public class AddPlayerInTheGameCommand : Notifiable, IRequest<AddPlayerInTheGameCommandResponseDTO>
    {
        public AddPlayerInTheGameCommand(string playerNickname, string gameInviteCode)
        {
            PlayerNickname = playerNickname;
            GameInviteCode = gameInviteCode;
        }

        public string PlayerNickname { get; private set; }
        public string GameInviteCode { get; private set; }


        public override async Task SubscribeRulesAsync(IMediator mediator, CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<AddPlayerInTheGameCommand>()
                .IsNotNullOrWhiteSpace(PlayerNickname, nameof(PlayerNickname), "Player nickname is required")
                .IsNotNullOrWhiteSpace(GameInviteCode, nameof(GameInviteCode), "Game invite code is required")
                .IsTrue(await GameInviteCodeIsValidAsync(mediator, cancellationToken), nameof(GameInviteCode), "Invalid game invite code"));
        }

        private async Task<bool> GameInviteCodeIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(new GetExistsGameByInviteCodeQuery(GameInviteCode), cancellationToken);
    }
}
