using Flunt.Validations;
using MediatR;
using PlanningPoker.Domain.DTOs;
using PlanningPoker.Domain.Notifications;
using PlanningPoker.Infra.Data.UnitOfWork;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommand : Notifiable, IRequest<CreateNewGameCommandResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewGameCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                .IsTrue(await DeckExistsAsync(), nameof(DeckId), "Deck entered not exists"));
        }

        private async Task<bool> DeckExistsAsync()
        {
            if (DeckId != Guid.Empty)
                return await _unitOfWork.DeckRepository.ExistsAsync(DeckId);
            return true;
        }
    }
}
