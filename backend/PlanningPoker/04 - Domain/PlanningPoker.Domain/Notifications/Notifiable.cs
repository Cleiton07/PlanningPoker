namespace PlanningPoker.Domain.Notifications
{
    public abstract class Notifiable : Flunt.Notifications.Notifiable<Flunt.Notifications.Notification>
    {
        public abstract Task SubscribeRulesAsync(CancellationToken cancellationToken = default);
    }
}
