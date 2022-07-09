using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.API.Models;
using PlanningPoker.Domain.Commands.AddDeck;
using PlanningPoker.Domain.Core.DTOs;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Controllers.v1
{
    using PostDeckResponse = Task<ActionResult<ResponseModel<AddDeckResponseDTO>>>;

    [Route("api/v1/decks")]
    [ApiController]
    public class DecksController : BaseController
    {
        public DecksController(IMediator mediator, Notifications.INotification notification) : base(mediator, notification)
        {
        }

        [HttpPost]
        public PostDeckResponse PostDeck([FromBody] AddDeckCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);
    }
}
