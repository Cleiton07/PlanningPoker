namespace PlanningPoker.Domain.Core.DTOs
{
    public class PlayerDTO
    {
        public PlayerDTO(Guid id, string name, bool excluded)
        {
            Id = id;
            Name = name;
            Excluded = excluded;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool Excluded { get; private set; }
    }
}
