using Flunt.Validations;

namespace PlanningPoker.Domain.Entities
{
    public class DeckItem : BaseEntity
    {
        public DeckItem(string value, int order)
        {
            SubscribeRules();

            Id = Guid.NewGuid();
            Value = value?.Trim();
            Order = order;
        }


        public Guid Id { get; private set; }
        public string Value { get; private set; }
        public int Order { get; private set; }


        protected override void SubscribeRules()
        {
            AddNotifications(new Contract<DeckItem>()
                .IsNotEmpty(Id, nameof(Id), "Id is required")
                .IsNotNullOrEmpty(Value, nameof(Value), "Value is required")
                .IsLowerOrEqualsThan(Value?.Length ?? 0, 3, nameof(Value), "Value has a maximum length of 3 characters"));
        }
    }
}
