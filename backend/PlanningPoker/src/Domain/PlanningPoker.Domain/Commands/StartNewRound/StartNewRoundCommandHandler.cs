using MediatR;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.GameQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Domain.Commands.StartNewRound
{
    public class StartNewRoundCommandHandler : IRequestHandler<StartNewRoundCommand, StartNewRoundCommandResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGamesWriteOnlyRepository _gamesWriteRepository;
        private readonly IMediator _mediator;

        public StartNewRoundCommandHandler(Notifications.INotification notification, IUnitOfWork unitOfWork,
            IGamesWriteOnlyRepository gamesWriteRepository, IMediator mediator)
        {
            _notification = notification;
            _unitOfWork = unitOfWork;
            _gamesWriteRepository = gamesWriteRepository;
            _mediator = mediator;
        }

        public async Task<StartNewRoundCommandResponseDTO> Handle(StartNewRoundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddFieldMessages(request, cancellationToken);
                if (!_notification.Successfully) return null;

                var activeRound = await _mediator.Send(new GetActiveRoundQuery(request.GameId), cancellationToken);
                if (activeRound != null)
                {
                    activeRound.Inactivate();
                    await _gamesWriteRepository.UpdateRoundAsync(activeRound, cancellationToken);
                }

                var round = new Round(request.GameId, request.RoundName);
                await _gamesWriteRepository.AddRoundAsync(round, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return new(round.Id, round.Name, round.GameId);
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
