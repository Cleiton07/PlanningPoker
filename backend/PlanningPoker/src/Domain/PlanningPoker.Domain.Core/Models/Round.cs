namespace PlanningPoker.Domain.Core.Models
{
    public class Round
    {
        public Round(Guid id, Guid gameId, string roundName, bool active)
        {
            SetInitialValues(id, gameId, roundName, active);
        }

        public Round(Guid gameId, string roundName)
        {
            SetInitialValues(Guid.NewGuid(), gameId, roundName, true);
        }

        public void SetInitialValues(Guid id, Guid gameId, string roundName, bool active)
        {
            Id = id;
            GameId = gameId;
            RoundName = roundName?.Trim();
            Active = active;
        }

        public Guid Id { get; private set; }
        public Guid GameId { get; private set; }
        public Game Game { get; private set; }
        public string RoundName { private get; set; }
        public bool Active { get; private set; }
    }
}
