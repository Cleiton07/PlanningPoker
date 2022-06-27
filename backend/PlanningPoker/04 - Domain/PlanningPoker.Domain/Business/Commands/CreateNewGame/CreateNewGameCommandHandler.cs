using MediatR;
using PlanningPoker.Domain.DTOs;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Domain.Interfaces.Repositories;

namespace PlanningPoker.Domain.Business.Commands.CreateNewGame
{
    public class CreateNewGameCommandHandler : IRequestHandler<CreateNewGameCommand, CreateNewGameCommandResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IDeckRepository _deckRepository;
        private readonly IGameRepository _gameRepository;

        public CreateNewGameCommandHandler(Notifications.INotification notification, IDeckRepository deckRepository, IGameRepository gameRepository)
        {
            _notification = notification;
            _deckRepository = deckRepository;
            _gameRepository = gameRepository;
        }

        public async Task<CreateNewGameCommandResponseDTO> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            _notification.AddNotificationFieldMessages(request);
            if (!_notification.Successfully) return null;

            var deck = await _deckRepository.GetByIdAsync(request.DeckId, cancellationToken);
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

            var gameId = await _gameRepository.AddAsync(game, cancellationToken);

            return new() { GameId = gameId, InviteCode = game.InviteCode };
        }
    }
}
