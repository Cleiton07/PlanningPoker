namespace PlanningPoker.Domain.Core.DTOs
{
    public class CreateNewGameCommandResponseDTO
    {
        public CreateNewGameCommandResponseDTO(Guid gameId, string inviteCode, Guid playerId)
        {
            GameId = gameId;
            InviteCode = inviteCode;
            PlayerId = playerId;
        }

        public Guid GameId { get; private set; }
        public string InviteCode { get; private set; }
        public Guid PlayerId { get; private set; }
    }
}
