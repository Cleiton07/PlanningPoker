using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.GameQueries;

namespace PlanningPoker.Domain.Commands.AddPlayerInTheGame
{
    public class AddPlayerInTheGameCommand : Notifiable, IRequest<StartGameResponseDTO>
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
            var gameInviteCodeValidationResult = await GameInviteCodeIsValidAsync(mediator, cancellationToken);

            AddNotifications(new Contract<AddPlayerInTheGameCommand>()
                .IsNotNullOrWhiteSpace(PlayerNickname, nameof(PlayerNickname), "Player nickname is required")
                .IsTrue(gameInviteCodeValidationResult.IsValid, nameof(GameInviteCode), gameInviteCodeValidationResult.Msg));
        }

        private async Task<(bool IsValid, string Msg)> GameInviteCodeIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(GameInviteCode))
                return (false, "Game invite code is required");
            if (!await mediator.Send(new GetExistsGameByInviteCodeQuery(GameInviteCode), cancellationToken))
                return (false, "There is no game with the given invite code");

            return (true, "");
        }
    }
}
