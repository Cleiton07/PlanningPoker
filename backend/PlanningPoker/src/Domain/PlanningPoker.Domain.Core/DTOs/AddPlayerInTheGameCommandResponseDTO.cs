namespace PlanningPoker.Domain.Core.DTOs
{
    public class AddPlayerInTheGameCommandResponseDTO
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
    }
}
