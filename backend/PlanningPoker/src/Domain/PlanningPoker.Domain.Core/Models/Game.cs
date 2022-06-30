using Flunt.Validations;
using PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Domain.Core.Models
{
    public class Game : Notifiable
    {
        public Game(string name)
        {
            Id = Guid.NewGuid();
            InviteCode = Guid.NewGuid().ToString();
            Name = name?.Trim();
            Deck = null;
            Players = new List<Player>();
        }


        public Guid Id { get; private set; }
        public string InviteCode { get; private set; }
        public string Name { get; private set; }
        public Deck Deck { get; private set; }
        public IReadOnlyCollection<Player> Players { get; private set; }


        public void SetDeck(Deck deck)
        {
            if (deck != null && deck.IsValid)
                Deck = deck;
        }

        public void AddPlayer(Player player)
        {
            if (player != null && player.IsValid)
                Players = Players.Append(player).ToList();
        }

        public void AddPlayers(IEnumerable<Player> players)
        {
            if (players != null && players.Any())
                foreach (var player in players) AddPlayer(player);
        }

        public override Task SubscribeRulesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<Game>()
                .IsNotEmpty(Id, nameof(Id), "Id is required")
                .IsNotNullOrWhiteSpace(Name, nameof(Name), "Name is required")
                .IsNotNullOrWhiteSpace(InviteCode, nameof(InviteCode), "Invite code is required")
                .IsNotNull(Deck, nameof(Deck), "Deck is required")
                .IsNotEmpty(Players, nameof(Player), "A game must have at least one player"));

            return Task.CompletedTask;
        }
    }
}
