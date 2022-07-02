using PlanningPoker.Domain.Core.Interfaces;

namespace PlanningPoker.Domain.Core.Models
{
    public class Player : IModel
    {
        public Player() { }

        public Player(Guid id, string nickname, Guid gameId)
        {
            SetInitialValues(id, nickname, gameId);
        }

        public Player(string nickname, Guid gameId)
        {
            SetInitialValues(Guid.NewGuid(), nickname, gameId);
        }

        private void SetInitialValues(Guid id, string nickname, Guid gameId)
        {
            Id = id;
            Nickname = nickname?.Trim();
            GameId = gameId;
        }


        public Guid Id { get; private set; }
        public string Nickname { get; private set; }
        public Guid GameId { get; private set; }
        public Game Game { get; private set; }
    }
}
