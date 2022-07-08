using MediatR;
using PlanningPoker.Domain.Builders.interfaces;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.GameQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Domain.Commands.AddPlayerInTheGame
{
    public class AddPlayerInTheGameCommandHandler : IRequestHandler<AddPlayerInTheGameCommand, StartGameResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IGamesWriteOnlyRepository _gamesWriteRepository;
        private readonly IStartGameResponseBuilder _startGameResponseBuilder;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public AddPlayerInTheGameCommandHandler(Notifications.INotification notification, IStartGameResponseBuilder startGameResponseBuilder,
            IGamesWriteOnlyRepository gamesWriteRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _notification = notification;
            _gamesWriteRepository = gamesWriteRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _startGameResponseBuilder = startGameResponseBuilder;
        }

        public async Task<StartGameResponseDTO> Handle(AddPlayerInTheGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddFieldMessages(request, cancellationToken);
                if (!_notification.Successfully) return null;

                var game = await _mediator.Send(new GetGameByInviteCodeQuery(request.GameInviteCode), cancellationToken);

                var player = new Player(request.PlayerNickname, game.Id);
                await _gamesWriteRepository.AddPlayerAsync(player, cancellationToken);

                var result = await _startGameResponseBuilder
                    .WithGame(game)
                    .WithPlayer(player)
                    .BuildAsync(cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return result;
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
