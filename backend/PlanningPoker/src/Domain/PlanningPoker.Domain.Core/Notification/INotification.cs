namespace PlanningPoker.Domain.Core.Notification
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
        Task AddFieldMessages(Notifiable notifiable, CancellationToken cancellationToken = default);
        void AddError(Exception ex);
        void Clear();
    }
}
