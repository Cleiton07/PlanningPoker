using Flunt.Notifications;

namespace PlanningPoker.Domain.Entities
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        protected abstract void SubscribeRules();
    }
}
