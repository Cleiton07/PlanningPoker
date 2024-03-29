﻿using MediatR;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.GameQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Domain.Commands.AddPlay
{
    public class AddPlayCommandHandler : IRequestHandler<AddPlayCommand>
    {
        private readonly Notifications.INotification _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IGamesWriteOnlyRepository _gamesWriteRepository;

        public AddPlayCommandHandler(Notifications.INotification notification, IUnitOfWork unitOfWork, IMediator mediator, 
            IGamesWriteOnlyRepository gamesWriteRepository)
        {
            _notification = notification;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _gamesWriteRepository = gamesWriteRepository;
        }

        public async Task<Unit> Handle(AddPlayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddFieldMessages(request, cancellationToken);
                if (!_notification.Successfully) return Unit.Value;

                var activeRound = await _mediator.Send(new GetActiveRoundQuery(request.GameId), cancellationToken);

                var play = new Play(activeRound.Id, request.PlayerId, request.DeckItemId);
                await _gamesWriteRepository.AddPlayAsync(play, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _notification.AddError(ex);
                _unitOfWork.Rollback();
                return Unit.Value;
            }
        }
    }
}
