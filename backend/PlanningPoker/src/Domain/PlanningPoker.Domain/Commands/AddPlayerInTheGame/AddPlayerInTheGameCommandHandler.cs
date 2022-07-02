using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.GameQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Domain.Commands.AddPlayerInTheGame
{
    public class AddPlayerInTheGameCommandHandler : IRequestHandler<AddPlayerInTheGameCommand, AddPlayerInTheGameCommandResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IGameRepository _gameRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public AddPlayerInTheGameCommandHandler(Notifications.INotification notification, IGameRepository gameRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _notification = notification;
            _gameRepository = gameRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddPlayerInTheGameCommandResponseDTO> Handle(AddPlayerInTheGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddNotificationFieldMessages(request);
                if (!_notification.Successfully) return null;

                var game = await _mediator.Send(new GetGameByInviteCodeQuery(request.GameInviteCode));

                var player = new Player(request.PlayerNickname, game.Id);
                await _gameRepository.AddPlayerAsync(player);

                await _unitOfWork.CommitAsync(cancellationToken);
                return new() { GameId = game.Id, PlayerId = player.Id };
            }
            catch (Exception ex)
            {
                _notification.AddError(ex);
                _unitOfWork.Rollback();
                return null;
            }
        }
    }
}
