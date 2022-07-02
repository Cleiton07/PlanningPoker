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

                await _notification.AddFieldMessages(request, cancellationToken);
                if (!_notification.Successfully) return null;

                var game = await _mediator.Send(new GetGameByInviteCodeQuery(request.GameInviteCode), cancellationToken);

                var player = new Player(request.PlayerNickname, game.Id);
                await _gameRepository.AddPlayerAsync(player, cancellationToken);

                var players = await _mediator.Send(new GetGamePlayersQuery(game.Id), cancellationToken);
                var rounds = await _mediator.Send(new GetGameRoundsQuery(game.Id), cancellationToken);
                var activeRound = await _mediator.Send(new GetActiveRoundQuery(game.Id), cancellationToken);
                var activeRoundPlays = activeRound is null 
                    ? null 
                    : await _mediator.Send(new GetRoundPlaysQuery(activeRound.Id), cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return new(player, game, activeRound, players, rounds, activeRoundPlays);
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
