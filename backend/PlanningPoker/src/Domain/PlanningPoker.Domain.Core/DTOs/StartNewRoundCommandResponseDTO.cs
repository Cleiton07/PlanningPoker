namespace PlanningPoker.Domain.Core.DTOs
{
    public class StartNewRoundCommandResponseDTO
    {
        public StartNewRoundCommandResponseDTO(Guid roundId, string roundName, Guid gameId)
        {
            RoundId = roundId;
            RoundName = roundName;
            GameId = gameId;
        }

        public Guid RoundId { get; private set; }
        public string RoundName { get; private set; }
        public Guid GameId { get; private set; }
    }
}
