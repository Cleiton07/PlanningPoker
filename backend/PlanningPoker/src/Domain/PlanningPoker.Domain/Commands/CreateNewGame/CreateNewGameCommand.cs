using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Notification;
using PlanningPoker.Domain.Queries.DeckQueries;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommand : Notifiable, IRequest<CreateNewGameCommandResponseDTO>
    {
        private readonly IMediator _mediator;

        public CreateNewGameCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Name { get; set; }
        public Guid DeckId { get; set; }
        public CreateNewGamePlayerDataDTO Player { get; set; }


        public override async Task SubscribeRulesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<CreateNewGameCommand>()
                .IsNotNullOrWhiteSpace(Name, nameof(Name), "Name is required")
                .IsNotNullOrWhiteSpace(Player?.Nickname, nameof(Player), "Player nickname is required")
                .IsNotNullOrWhiteSpace(Player?.ConnectionId, nameof(Player), "Player connection is required")
                .IsNotEmpty(DeckId, nameof(DeckId), "Deck is required")
                .IsTrue(DeckId != Guid.Empty && await DeckExistsAsync(), nameof(DeckId), "Deck entered not exists"));
        }

        private async Task<bool> DeckExistsAsync()
        {
            return await _mediator.Send(new GetExistsDeckByIdQuery(DeckId));
        }
    }
}
