using Flunt.Validations;

namespace PlanningPoker.Domain.Entities
{
    public class Player : BaseEntity
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


        protected override void SubscribeRules()
        {
            AddNotifications(new Contract<Player>()
                .IsNotEmpty(Id, nameof(Id), "Id is required")
                .IsNotNullOrWhiteSpace(Nickname, nameof(Nickname), "Nickname is required")
                .IsNotNullOrWhiteSpace(ConnectionId, nameof(ConnectionId), "Connection id is required"));
        }
    }
}
