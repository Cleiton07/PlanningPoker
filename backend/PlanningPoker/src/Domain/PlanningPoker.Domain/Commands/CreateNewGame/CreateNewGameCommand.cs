using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.DeckQueries;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommand : Notifiable, IRequest<StartGameResponseDTO>
    {
        public CreateNewGameCommand(string name, Guid deckId, string playerNickname)
        {
            Name = name;
            DeckId = deckId;
            PlayerNickname = playerNickname;
        }

        public string Name { get; private set; }
        public Guid DeckId { get; private set; }
        public string PlayerNickname { get; private set; }


        public override async Task SubscribeRulesAsync(IMediator mediator, CancellationToken cancellationToken = default)
        {
            var deckIdValidationResult = await DeckIsValidAsync(mediator, cancellationToken);

            AddNotifications(new Contract<CreateNewGameCommand>()
                .IsNotNullOrWhiteSpace(Name, nameof(Name), "Name is required")
                .IsNotNullOrWhiteSpace(PlayerNickname, nameof(PlayerNickname), "Player nickname is required")
                .IsTrue(deckIdValidationResult.IsValid, nameof(DeckId), deckIdValidationResult.Msg));
        }

        private async Task<(bool IsValid, string Msg)> DeckIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (DeckId == Guid.Empty)
                return (false, "Deck id is required");
            if (!await mediator.Send(new GetExistsDeckByIdQuery(DeckId), cancellationToken))
                return (false, "There is no deck with the given id");

            return (true, "");
        }
    }
}
