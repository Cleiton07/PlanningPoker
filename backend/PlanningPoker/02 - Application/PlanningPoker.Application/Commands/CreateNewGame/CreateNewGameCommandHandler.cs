using MediatR;
using PlanningPoker.Domain.DTOs;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Infra.Data.UnitOfWork;
using Notifications = PlanningPoker.Domain.Notifications;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommandHandler : IRequestHandler<CreateNewGameCommand, CreateNewGameCommandResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewGameCommandHandler(Notifications.INotification notification, IUnitOfWork unitOfWork)
        {
            _notification = notification;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateNewGameCommandResponseDTO> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                _notification.AddNotificationFieldMessages(request);
                if (!_notification.Successfully) return null;

                var deck = await _unitOfWork.DeckRepository.GetByIdAsync(request.DeckId, cancellationToken);
                _notification.AddNotificationMessages(deck);
                if (!_notification.Successfully) return null;

                var player = new Player(request.Player.Nickname, request.Player.ConnectionId);
                _notification.AddNotificationMessages(player);
                if (!_notification.Successfully) return null;

                var game = new Game(request.Name);
                game.SetDeck(deck);
                game.AddPlayer(player);

                _notification.AddNotificationMessages(game);
                if (!_notification.Successfully) return null;

                var gameId = await _unitOfWork.GameRepository.AddAsync(game, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return new() { GameId = gameId, InviteCode = game.InviteCode };
            }
            catch(Exception ex)
            {
                _notification.AddError(ex);
                _unitOfWork.Dispose();
                return null;
            }
        }
    }
}
