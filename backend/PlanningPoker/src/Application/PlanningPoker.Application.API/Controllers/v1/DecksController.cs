using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.API.Models;
using PlanningPoker.Domain.Commands.AddDeck;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Queries.DeckQueries;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Controllers.v1
{
    using GetDeckByIdResponse = ActionResult<ResponseModel<DeckDTO>>;
    using GetDecksResponse = ActionResult<ResponseModel<GetDecksQueryResponseDTO>>;
    using PostDeckResponse = ActionResult<ResponseModel<AddDeckResponseDTO>>;

    [Route("api/v1/decks")]
    [ApiController]
    public class DecksController : BaseController
    {
        public DecksController(IMediator mediator, Notifications.INotification notification) : base(mediator, notification)
        {
        }

        [HttpPost]
        public Task<PostDeckResponse> PostDeckAsync([FromBody] AddDeckCommand command, CancellationToken cancellationToken)
            => ExecuteAsync(command, cancellationToken);

        [HttpGet]
        public Task<GetDecksResponse> GetDecksAsync(string search, int page, CancellationToken cancellationToken)
            => ExecuteAsync(new GetDecksQuery(search, page), cancellationToken);

        [HttpGet("{id}")]
        public Task<GetDeckByIdResponse> GetDeckByIdAsync(Guid id, CancellationToken cancellationToken)
            => ExecuteAsync(new GetDeckDTOByIdQuery(id), cancellationToken);
    }
}
