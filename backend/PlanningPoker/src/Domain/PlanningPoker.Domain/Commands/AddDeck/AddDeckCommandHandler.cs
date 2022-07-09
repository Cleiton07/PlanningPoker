using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories;
using PlanningPoker.Domain.Core.Models;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Domain.Commands.AddDeck
{
    public class AddDeckCommandHandler : IRequestHandler<AddDeckCommand, AddDeckResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDecksWriteOnlyRepository _decksWriteRepository;

        public AddDeckCommandHandler(Notifications.INotification notification, IUnitOfWork unitOfWork, IDecksWriteOnlyRepository decksWriteRepository)
        {
            _notification = notification;
            _unitOfWork = unitOfWork;
            _decksWriteRepository = decksWriteRepository;
        }

        public async Task<AddDeckResponseDTO> Handle(AddDeckCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddFieldMessages(request, cancellationToken);
                if (!_notification.Successfully) return null;

                var deck = new Deck(request.DeckName);
                foreach (var item in request.Items)
                    deck.AddItem(new DeckItem(item.Value, request.Items.IndexOf(item) + 1, deck.Id));

                await _decksWriteRepository.AddAsync(deck, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return new(deck.Id, deck.Name, deck.Items.Select(item => new DeckItemDTO(item.Id, item.Value, item.Order)).ToList());
            }
            catch(Exception ex)
            {
                _notification.AddError(ex);
                _unitOfWork.Rollback();
                return null;
            }
        }
    }
}
