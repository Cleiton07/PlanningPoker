namespace PlanningPoker.Domain.Core.Models
{
    public class Play
    {
        public Play() { }

        public Play(Guid id, Guid roundId, Guid playerId, Guid deckItemId)
        {
            SetInitialValues(id, roundId, playerId, deckItemId);
        }

        public Play(Guid roundId, Guid playerId, Guid deckItemId)
        {
            SetInitialValues(Guid.NewGuid(), roundId, playerId, deckItemId);
        }

        private void SetInitialValues(Guid id, Guid roundId, Guid playerId, Guid deckItemId)
        {
            Id = id;
            RoundId = roundId;
            PlayerId = playerId;
            DeckItemId = deckItemId;
        }


        public Guid Id { get; private set; }
        public Guid RoundId { get; private set; }
        public Round Round { get; private set; }
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }
        public Guid DeckItemId { get; private set; }
        public DeckItem DeckItem { get; private set; }
        public DateTime DateTimeOfPlay { get; private set; }

    }
}
