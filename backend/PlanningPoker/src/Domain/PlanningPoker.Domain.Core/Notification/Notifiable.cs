namespace PlanningPoker.Domain.Core.Notification
{
    public abstract class Notifiable : Flunt.Notifications.Notifiable<Flunt.Notifications.Notification>
    {
        public abstract Task SubscribeRulesAsync(CancellationToken cancellationToken = default);
    }
}
