namespace PlanningPoker.Domain.Core.Models
{
    public class Deck
    {
        public Deck(Guid id, string name, IEnumerable<DeckItem> items)
        {
            SetInitialValues(id, name, items);
        }

        public Deck(string name, IEnumerable<DeckItem> items)
        {
            SetInitialValues(Guid.NewGuid(), name, items);
        }

        private void SetInitialValues(Guid id, string name, IEnumerable<DeckItem> items)
        {
            Id = id;
            Name = name?.Trim();
            Items = items?.ToList() ?? new List<DeckItem>();
        }


        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IList<DeckItem> Items { get; private set; }
        public IList<Game> Games { get; private set; }


        public void AddItem(DeckItem item)
        {
            if (item != null)
                Items.Add(item);
        }

        public void AddItems(IEnumerable<DeckItem> items)
        {
            if (items != null && items.Any())
                foreach (var item in items) AddItem(item);
        }
    }
}
