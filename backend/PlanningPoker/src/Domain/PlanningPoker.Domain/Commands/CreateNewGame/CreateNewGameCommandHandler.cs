using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.DeckQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommandHandler : IRequestHandler<CreateNewGameCommand, CreateNewGameCommandResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;

        public CreateNewGameCommandHandler(Notifications.INotification notification, IMediator mediator,
            IUnitOfWork unitOfWork, IGameRepository gameRepository)
        {
            _notification = notification;
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _mediator = mediator;
        }

        public async Task<CreateNewGameCommandResponseDTO> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddNotificationFieldMessages(request);
                if (!_notification.Successfully) return null;

                var deck = await _mediator.Send(new GetDeckByIdQuery(request.DeckId));
                await _notification.AddNotificationMessages(deck);
                if (!_notification.Successfully) return null;

                var player = new Player(request.Player.Nickname, request.Player.ConnectionId);
                await _notification.AddNotificationMessages(player);
                if (!_notification.Successfully) return null;

                var game = new Game(request.Name);
                game.SetDeck(deck);
                game.AddPlayer(player);

                await _notification.AddNotificationMessages(game);
                if (!_notification.Successfully) return null;

                var gameId = await _gameRepository.AddAsync(game, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return new() { GameId = gameId, InviteCode = game.InviteCode };
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
