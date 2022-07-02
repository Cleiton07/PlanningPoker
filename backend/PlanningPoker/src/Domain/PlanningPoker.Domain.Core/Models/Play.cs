namespace PlanningPoker.Domain.Core.Models
{
    public class Play
    {
        public Play(Guid id, Guid gameId, Guid playerId, Guid deckItemId)
        {
            SetInitialValues(id, gameId, playerId, deckItemId);
        }

        public Play(Guid gameId, Guid playerId, Guid deckItemId)
        {
            SetInitialValues(Guid.NewGuid(), gameId, playerId, deckItemId);
        }

        private void SetInitialValues(Guid id, Guid gameId, Guid playerId, Guid deckItemId)
        {
            Id = id;
            GameId = gameId;
            PlayerId = playerId;
            DeckItemId = deckItemId;
        }


        public Guid Id { get; private set; }
        public Guid GameId { get; private set; }
        public Game Game { get; private set; }
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }
        public Guid DeckItemId { get; private set; }
        public DeckItem DeckItem { get; private set; }
        public DateTime DateTimeOfPlay { get; private set; }

    }
}
