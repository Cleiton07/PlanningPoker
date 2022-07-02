namespace PlanningPoker.Domain.Core.DTOs
{
    public class PlayerPlayDTO
    {
        public PlayerPlayDTO(Guid roundId, string roundName, Guid playerId, string playerNickname, Guid deckItemId, 
            string deckItemValue, DateTime dateTimeOfPlay)
        {
            RoundId = roundId;
            RoundName = roundName;

            PlayerId = playerId;
            PlayerNickname = playerNickname;

            DeckItemId = deckItemId;
            DeckItemValue = deckItemValue;

            DateTimeOfPlay = dateTimeOfPlay;
        }

        public Guid RoundId { get; private set; }
        public string RoundName { get; private set; }
        public Guid PlayerId { get; private set; }
        public string PlayerNickname { get; private set; }
        public Guid DeckItemId { get; private set; }
        public string DeckItemValue { get; private set; }
        public DateTime DateTimeOfPlay { get; private set; }
    }
}
