namespace PlanningPoker.Domain.Core.Models
{
    public class Round
    {
        public Round(Guid id, Guid gameId, string roundName)
        {
            SetInitialValues(id, gameId, roundName);
        }
        public Round(Guid gameId, string roundName)
        {
            SetInitialValues(Guid.NewGuid(), gameId, roundName);
        }

        public void SetInitialValues(Guid id, Guid gameId, string roundName)
        {
            Id = id;
            GameId = gameId;
            RoundName = roundName?.Trim();
        }

        public Guid Id { get; private set; }
        public Guid GameId { get; private set; }
        public Game Game { get; private set; }
        public string RoundName { private get; set; }
    }
}
