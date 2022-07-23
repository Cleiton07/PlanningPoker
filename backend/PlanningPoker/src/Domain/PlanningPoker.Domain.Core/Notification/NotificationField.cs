namespace PlanningPoker.Domain.Core.Notification
{
    public class NotificationField
    {
        public string FieldName { get; private set; }
        public IReadOnlyCollection<string> Messages { get; private set; }


        public NotificationField(string fieldName, IEnumerable<string> messages)
        {
            FieldName = fieldName?.Trim();

            Messages = new List<string>();
            AddMessages(messages);
        }

        public NotificationField(string fieldName, string message)
        {
            FieldName = fieldName?.Trim();

            Messages = new List<string>();
            AddMessage(message);
        }


        public void AddMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Messages = Messages.Append(message.Trim()).ToList();
        }

        public void AddMessages(IEnumerable<string> messages)
        {
            foreach (var msg in messages) AddMessage(msg);
        }
    }
}
