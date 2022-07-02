namespace PlanningPoker.Domain.Core.DTOs
{
    public class RoundDTO
    {
        public RoundDTO(Guid id, string name, bool active)
        {
            Id = id;
            Name = name;
            Active = active;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }
    }
}
