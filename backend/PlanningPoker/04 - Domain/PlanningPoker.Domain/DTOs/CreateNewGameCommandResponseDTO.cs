namespace PlanningPoker.Domain.DTOs
{
    public class CreateNewGameCommandResponseDTO
    {
        public Guid GameId { get; set; }
        public string InviteCode { get; set; }
    }
}
