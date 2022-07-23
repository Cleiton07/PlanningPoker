using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.API.Models;
using PlanningPoker.Application.Commands.CreateNewGame;
using PlanningPoker.Domain.Commands.AddPlay;
using PlanningPoker.Domain.Commands.AddPlayerInTheGame;
using PlanningPoker.Domain.Commands.StartNewRound;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Queries.GameQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Controllers.v1
{
    using GetRoundPlaysResponse = ActionResult<ResponseModel<IList<PlayerPlayDTO>>>;
    using PostGameResponse = ActionResult<ResponseModel<StartGameResponseDTO>>;
    using PostPlayerResponse = ActionResult<ResponseModel<StartGameResponseDTO>>;
    using PostPlayResponse = ActionResult<ResponseModel<Unit>>;
    using PostRoundResponse = ActionResult<ResponseModel<StartNewRoundCommandResponseDTO>>;

    [Route("api/v1/games")]
    [ApiController]
    public class GamesController : BaseController
    {
        public GamesController(IMediator mediator, Notifications.INotification notification) : base(mediator, notification)
        {
        }

        [HttpPost]
        public Task<PostGameResponse> PostGameAsync([FromBody] CreateNewGameCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpPost("players")]
        public Task<PostPlayerResponse> PostPlayerAsync([FromBody] AddPlayerInTheGameCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpPost("plays")]
        public Task<PostPlayResponse> PostPlayAsync([FromBody] AddPlayCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpPost("rounds")]
        public Task<PostRoundResponse> PostRoundAsync([FromBody] StartNewRoundCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpGet("rounds/{roundId}/plays")]
        public Task<GetRoundPlaysResponse> GetRoundPlaysAsync(Guid roundId , CancellationToken cancellationToken)
            => ExecuteAsync(new GetRoundPlaysQuery(roundId), cancellationToken);
    }
}
