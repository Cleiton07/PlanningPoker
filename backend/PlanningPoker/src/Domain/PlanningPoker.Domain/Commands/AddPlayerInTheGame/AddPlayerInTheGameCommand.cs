using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.GameQueries;

namespace PlanningPoker.Domain.Commands.AddPlayerInTheGame
{
    public class AddPlayerInTheGameCommand : Notifiable, IRequest<AddPlayerInTheGameCommandResponseDTO>
    {
        private readonly IMediator _mediator;

        public AddPlayerInTheGameCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string PlayerNickname { get; set; }
        public string GameInviteCode { get; set; }


        public override async Task SubscribeRulesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<AddPlayerInTheGameCommand>()
                .IsNotNullOrWhiteSpace(PlayerNickname, nameof(PlayerNickname), "Player nickname is required")
                .IsNotNullOrWhiteSpace(GameInviteCode, nameof(GameInviteCode), "Game invite code is required")
                .IsTrue(await GameInviteCodeIsValidAsync(cancellationToken), nameof(GameInviteCode), "Invalid game invite code"));
        }

        private async Task<bool> GameInviteCodeIsValidAsync(CancellationToken cancellationToken)
            => await _mediator.Send(new GetExistsGameByInviteCodeQuery(GameInviteCode), cancellationToken);
    }
}
