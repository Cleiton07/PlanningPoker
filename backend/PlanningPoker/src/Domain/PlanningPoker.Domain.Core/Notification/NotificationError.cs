namespace PlanningPoker.Domain.Core.Notification
{
    public class NotificationError
    {
        public string Type { get; private set; }
        public string Message { get; private set; }
        public NotificationError InnerError { get; private set; }

        public NotificationError(Exception ex)
        {
            if (ex != null)
            {
                Type = ex.GetType().Name;
                Message = string.IsNullOrWhiteSpace(ex.Message) ? "Unexpected error without message" : ex.Message;
                if (ex.InnerException != null)
                    InnerError = new(ex.InnerException);
            }
        }
    }
}
