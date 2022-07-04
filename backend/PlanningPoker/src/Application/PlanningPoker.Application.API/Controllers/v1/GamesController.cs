using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.API.Models;
using PlanningPoker.Application.Commands.CreateNewGame;
using PlanningPoker.Domain.Commands.AddPlay;
using PlanningPoker.Domain.Commands.AddPlayerInTheGame;
using PlanningPoker.Domain.Commands.StartNewRound;
using PlanningPoker.Domain.Core.DTOs;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Controllers.v1
{
    using PostGameResponse = Task<ActionResult<ResponseModel<StartGameResponseDTO>>>;
    using PostPlayerResponse = Task<ActionResult<ResponseModel<StartGameResponseDTO>>>;
    using PostPlayResponse = Task<ActionResult<ResponseModel<Unit>>>;
    using PostRoundResponse = Task<ActionResult<ResponseModel<StartNewRoundCommandResponseDTO>>>;

    [Route("api/v1/games")]
    [ApiController]
    public class GamesController : BaseController
    {
        public GamesController(IMediator mediator, Notifications.INotification notification) : base(mediator, notification)
        {
        }

        [HttpPost]
        public PostGameResponse PostGame([FromBody] CreateNewGameCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpPost("players")]
        public PostPlayerResponse PostPlayer([FromBody] AddPlayerInTheGameCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpPost("plays")]
        public PostPlayResponse PostPlay([FromBody] AddPlayCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpPost("rounds")]
        public PostRoundResponse PostRound([FromBody] StartNewRoundCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);
    }
}
