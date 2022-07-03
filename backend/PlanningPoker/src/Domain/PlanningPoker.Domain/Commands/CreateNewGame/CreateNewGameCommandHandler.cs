﻿using MediatR;
using PlanningPoker.Domain.Builders;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.Commands.CreateNewGame
{
    public class CreateNewGameCommandHandler : IRequestHandler<CreateNewGameCommand, StartGameResponseDTO>
    {
        private readonly Notifications.INotification _notification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;
        private readonly IMediator _mediator;

        public CreateNewGameCommandHandler(Notifications.INotification notification, IUnitOfWork unitOfWork, IMediator mediator, 
            IGameRepository gameRepository)
        {
            _notification = notification;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _gameRepository = gameRepository;
        }

        public async Task<StartGameResponseDTO> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

                await _notification.AddFieldMessages(request, cancellationToken);
                if (!_notification.Successfully) return null;

                var game = new Game(request.Name, request.DeckId);
                await _gameRepository.AddAsync(game, cancellationToken);

                var player = new Player(request.PlayerNickname, game.Id);
                await _gameRepository.AddPlayerAsync(player, cancellationToken);

                var result = await new StartGameResponseBuilder(_mediator)
                    .WithGame(game).WithPlayer(player)
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
