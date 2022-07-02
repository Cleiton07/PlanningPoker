using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.DeckQueries;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommand : Notifiable, IRequest<CreateNewGameCommandResponseDTO>
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
            AddNotifications(new Contract<CreateNewGameCommand>()
                .IsNotNullOrWhiteSpace(Name, nameof(Name), "Name is required")
                .IsNotNullOrWhiteSpace(PlayerNickname, nameof(PlayerNickname), "Player nickname is required")
                .IsTrue(await DeckIsValidAsync(mediator, cancellationToken), nameof(DeckId), "Deck is not valid"));
        }

        private async Task<bool> DeckIsValidAsync(IMediator mediator, CancellationToken cancellationToken)
        {
            if (DeckId == Guid.Empty) return false;
            return await mediator.Send(new GetExistsDeckByIdQuery(DeckId), cancellationToken);
        }
    }
}
