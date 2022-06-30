using PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Application.API.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel(INotification notification, T data)
        {
            Errors = notification.Errors.ToList();
            FieldMessages = notification.FieldMessages.ToList();
            Messages = notification.Messages.ToList();
            Data = data;
        }

        IList<NotificationError> Errors { get; set; }
        IList<NotificationField> FieldMessages { get; set; }
        IList<string> Messages { get; set; }
        public bool Successfully => !Errors.Any() && !FieldMessages.Any() && !Messages.Any();
        public T Data { get; set; }
    }
}
