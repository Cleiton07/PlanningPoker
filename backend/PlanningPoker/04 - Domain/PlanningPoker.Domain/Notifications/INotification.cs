namespace PlanningPoker.Domain.Notifications
{
    public interface INotification
    {
        bool Successfully { get; }
        IReadOnlyCollection<string> Messages { get; }
        IReadOnlyCollection<NotificationField> FieldMessages { get; }
        IReadOnlyCollection<NotificationError> Errors { get; }

        void AddMessage(string message);
        void AddMessages(IEnumerable<string> messages);
        void AddFieldMessage(NotificationField fieldMessage);
        void AddFieldMessages(IEnumerable<NotificationField> fieldMessages);
        void AddNotificationFieldMessages(Notifiable notifiable);
        void AddNotificationMessages(Notifiable notifiable);
        void AddError(Exception ex);
        void Clear();
    }
}
