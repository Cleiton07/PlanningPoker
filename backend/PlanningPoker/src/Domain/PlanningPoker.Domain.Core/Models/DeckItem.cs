namespace PlanningPoker.Domain.Core.Models
{
    public class DeckItem
    {
        public DeckItem() { }

        public DeckItem(Guid id, string value, int order, Guid deckId)
        {
            SetInitialValues(id, value, order, deckId);
        }

        public DeckItem(string value, int order, Guid deckId)
        {
            SetInitialValues(Guid.NewGuid(), value, order, deckId);
        }

        private void SetInitialValues(Guid id, string value, int order, Guid deckId)
        {
            Id = id;
            Value = value?.Trim();
            Order = order;
            DeckId = deckId;
        }


        public Guid Id { get; private set; }
        public string Value { get; private set; }
        public int Order { get; private set; }
        public Guid DeckId { get; private set; }
        public Deck Deck { get; private set; }
    }
}
