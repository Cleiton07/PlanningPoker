namespace PlanningPoker.Domain.Core.DTOs
{
    public class DeckItemDTO
    {
        public DeckItemDTO(Guid id, string value, int order)
        {
            Id = id;
            Value = value;
            Order = order;
        }

        public Guid Id { get; private set; }
        public string Value { get; private set; }
        public int Order { get; private set; }
    }
}
