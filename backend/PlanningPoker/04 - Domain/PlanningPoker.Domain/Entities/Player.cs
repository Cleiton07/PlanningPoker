using Flunt.Validations;
using PlanningPoker.Domain.Notifications;

namespace PlanningPoker.Domain.Entities
{
    public class Player : Notifiable
    {
        public Player(string nickname, string connectionId)
        {
            Id = Guid.NewGuid();
            Nickname = nickname?.Trim();
            ConnectionId = connectionId?.Trim();
        }


        public Guid Id { get; private set; }
        public string Nickname { get; private set; }
        public string ConnectionId { get; private set; }


        public override Task SubscribeRulesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications(new Contract<Player>()
                .IsNotEmpty(Id, nameof(Id), "Id is required")
                .IsNotNullOrWhiteSpace(Nickname, nameof(Nickname), "Nickname is required")
                .IsNotNullOrWhiteSpace(ConnectionId, nameof(ConnectionId), "Connection id is required"));

            return Task.CompletedTask;
        }
    }
}
