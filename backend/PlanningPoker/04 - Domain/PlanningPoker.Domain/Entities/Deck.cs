using Flunt.Validations;
using PlanningPoker.Domain.Notifications;

namespace PlanningPoker.Domain.Entities
{
    public class Deck : Notifiable
    {
        public Deck(string name)
        {
            Id = Guid.NewGuid();
            Name = name?.Trim();
            Items = new List<DeckItem>();
        }


        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<DeckItem> Items { get; private set; }


        public void AddItem(DeckItem item)
        {
            if (item != null && item.IsValid)
                Items = Items.Append(item).ToList();
        }

        public void AddItems(IEnumerable<DeckItem> items)
        {
            if (items != null && items.Any())
                foreach (var item in items) AddItem(item);
        }


        private bool HasARepeatedItemValue()
        {
            return Items.Any((item) => Items.Where((comparerItem) => item.Value == comparerItem.Value).Count() >= 2);
        }

        private bool HasARepeatedItemOrder()
        {
            return Items.Any((item) => Items.Where((comparerItem) => item.Order == comparerItem.Order).Count() >= 2);
        }


        public override Task SubscribeRulesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<Deck>()
                .IsNotEmpty(Id, nameof(Id), "Id is required")
                .IsNotNullOrWhiteSpace(Name, nameof(Name), "Name is required")
                .IsGreaterOrEqualsThan(Items.Count, 2, nameof(Items), "A deck must have at least two items")
                .IsLowerOrEqualsThan(Items.Count, 30, nameof(Items), "A deck must have a maximum of thirty items")
                .IsFalse(HasARepeatedItemValue(), nameof(Items), "There cannot be items with repeated values")
                .IsFalse(HasARepeatedItemOrder(), nameof(Items), "There cannot be items with repeated orders"));

            return Task.CompletedTask;
        }
    }
}
