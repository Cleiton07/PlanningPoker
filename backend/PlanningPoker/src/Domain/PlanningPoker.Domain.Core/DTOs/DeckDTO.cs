namespace PlanningPoker.Domain.Core.DTOs
{
    public class DeckDTO
    {
        public DeckDTO(Guid id, string name, IList<DeckItemDTO> items)
        {
            Id = id;
            Name = name;
            Items = items;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IList<DeckItemDTO> Items { get; private set; }
    }
}
