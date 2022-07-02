using MediatR;

namespace PlanningPoker.Domain.Core.Notification
{
    public class Notification : INotification
    {
        private readonly IMediator _mediator;

        public Notification(IMediator mediator)
        {
            Messages = new List<string>();
            FieldMessages = new List<NotificationField>();
            Errors = new List<NotificationError>();
            _mediator = mediator;
        }


        public IReadOnlyCollection<NotificationError> Errors { get; private set; }
        public IReadOnlyCollection<NotificationField> FieldMessages { get; private set; }
        public IReadOnlyCollection<string> Messages { get; private set; }
        public bool Successfully => !Errors.Any() && !FieldMessages.Any() && !Messages.Any();


        public void AddError(Exception ex)
        {
            if (ex != null)
            {
                var newErrors = new List<NotificationError>();
                newErrors.AddRange(Errors);
                newErrors.Add(new(ex));
                Errors = newErrors;
            }
        }

        public void AddFieldMessage(NotificationField fieldMessage)
        {
            if (!string.IsNullOrWhiteSpace(fieldMessage?.FieldName) && fieldMessage?.Messages?.Any() == true)
            {
                var existentFieldMessage = FieldMessages.FirstOrDefault(fm => fm.FieldName == fieldMessage.FieldName);
                if (existentFieldMessage is null)
                {
                    var newFieldMessages = new List<NotificationField>();
                    newFieldMessages.AddRange(FieldMessages);
                    newFieldMessages.Add(fieldMessage);
                    FieldMessages = newFieldMessages;
                }
                else
                {
                    existentFieldMessage.AddMessages(fieldMessage.Messages);
                }
            }
        }

        public void AddFieldMessages(IEnumerable<NotificationField> fieldMessages)
        {
            if (fieldMessages != null)
                foreach (var fieldMessage in fieldMessages)
                    AddFieldMessage(fieldMessage);
        }

        public async Task AddFieldMessages(Notifiable notifiable, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

            await notifiable.SubscribeRulesAsync(_mediator, cancellationToken);
            if (notifiable.IsValid) return;

            foreach (var message in notifiable.Notifications)
                AddFieldMessage(new NotificationField(message.Key, message.Message));
        }

        public void AddMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                var newMessages = new List<string>();
                newMessages.AddRange(Messages);
                newMessages.Add(message);
                Messages = newMessages;
            }
        }

        public void AddMessages(IEnumerable<string> messages)
        {
            if (messages != null)
                foreach (var message in messages)
                    AddMessage(message);
        }

        public void Clear()
        {
            Messages = new List<string>();
            FieldMessages = new List<NotificationField>();
        }
    }
}
