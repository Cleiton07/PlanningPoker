using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.API.Models;
using PlanningPoker.Application.Commands.CreateNewGame;
using PlanningPoker.Domain.Core.DTOs;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Controllers.v1
{
    using PostNewGameResponse = Task<ActionResult<ResponseModel<CreateNewGameCommandResponseDTO>>>;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : BaseController
    {
        public GamesController(IMediator mediator, Notifications.INotification notification) : base(mediator, notification)
        {
        }

        [HttpPost]
        public PostNewGameResponse PostNewGame(CreateNewGameCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);
    }
}
