using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.Commands.CreateNewGame;
using PlanningPoker.Domain.DTOs;
using PlanningPoker.Presentation.API.Models;
using Notifications = PlanningPoker.Domain.Notifications;

namespace PlanningPoker.Presentation.API.Controllers.v1
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
