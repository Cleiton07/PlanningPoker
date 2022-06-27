using Flunt.Validations;
using PlanningPoker.Domain.Notifications;

namespace PlanningPoker.Domain.Entities
{
    public class DeckItem : Notifiable
    {
        public DeckItem(string value, int order)
        {
            Id = Guid.NewGuid();
            Value = value?.Trim();
            Order = order;
        }


        public Guid Id { get; private set; }
        public string Value { get; private set; }
        public int Order { get; private set; }


        public override Task SubscribeRulesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<DeckItem>()
                .IsNotEmpty(Id, nameof(Id), "Id is required")
                .IsNotNullOrEmpty(Value, nameof(Value), "Value is required")
                .IsLowerOrEqualsThan(Value?.Length ?? 0, 3, nameof(Value), "Value has a maximum length of 3 characters"));

            return Task.CompletedTask;
        }
    }
}
