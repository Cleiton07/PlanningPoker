namespace PlanningPoker.Domain.Core.DTOs
{
    public class AddPlayerInTheGameCommandResponseDTO
    {
        public AddPlayerInTheGameCommandResponseDTO(Guid playerId, Guid gameId)
        {
            PlayerId = playerId;
            GameId = gameId;
        }

        public Guid PlayerId { get; private set; }
        public Guid GameId { get; private set; }
    }
}
