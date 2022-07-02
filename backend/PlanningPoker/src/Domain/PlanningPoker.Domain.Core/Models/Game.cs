using PlanningPoker.Domain.Core.Interfaces;

namespace PlanningPoker.Domain.Core.Models
{
    public class Game : IModel
    {
        public Game() { }

        public Game(Guid id, string name, Guid deckId)
        {
            SetInitialValues(id, name, Guid.NewGuid().ToString(), deckId);
        }

        public Game(string name, Guid deckId)
        {
            SetInitialValues(Guid.NewGuid(), name, Guid.NewGuid().ToString(), deckId);
        }

        private void SetInitialValues(Guid id, string name, string inviteCode, Guid deckId)
        {
            Id = id;
            Name = name?.Trim();
            InviteCode = inviteCode?.Trim();
            DeckId = deckId;
            Players = new List<Player>();
        }


        public Guid Id { get; private set; }
        public string InviteCode { get; private set; }
        public string Name { get; private set; }
        public Guid DeckId { get; private set; }
        public Deck Deck { get; private set; }
        public IList<Player> Players { get; private set; }
    }
}
