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

        public IList<NotificationError> Errors { get; set; }
        public IList<NotificationField> FieldMessages { get; set; }
        public IList<string> Messages { get; set; }
        public bool Successfully => !Errors.Any() && !FieldMessages.Any() && !Messages.Any();
        public T Data { get; set; }
    }
}
