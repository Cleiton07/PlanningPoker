using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Application.API.Models;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Notifications.INotification _notification;

        public BaseController(IMediator mediator, Notifications.INotification notification)
        {
            _notification = notification;
            _mediator = mediator;
        }

        protected async Task<ActionResult<ResponseModel<T>>> ExecuteAsync<T>(IRequest<T> command, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);
            return await ExecuteAsync(() => _mediator.Send(command, cancellationToken), cancellationToken);
        }

        protected async Task<ActionResult<ResponseModel<T>>> ExecuteAsync<T>(Func<Task<T>> func, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

            T respData = default;
            try
            {
                respData = await func();
            }
            catch (Exception ex)
            {
                _notification.AddError(ex);
            }

            return BuildResponse(respData);
        }

        private ActionResult<ResponseModel<T>> BuildResponse<T>(T data)
        {
            var resp = new ResponseModel<T>(_notification, data);
            if (_notification.Successfully) return Ok(resp);
            else if (_notification.Errors.Any()) return InternalServerError(resp);
            else return BadRequest(resp);
        }

        protected ActionResult<T> InternalServerError<T>(T resp)
            => StatusCode(StatusCodes.Status500InternalServerError, resp);
    }
}
