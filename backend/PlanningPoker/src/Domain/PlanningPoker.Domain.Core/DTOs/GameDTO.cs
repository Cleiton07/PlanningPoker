namespace PlanningPoker.Domain.Core.DTOs
{
    public class GameDTO
    {
        public GameDTO(Guid id, string name, DeckDTO deck, IList<PlayerDTO> players, IList<RoundDTO> rounds, IList<PlayerPlayDTO> activeRoundPlays)
        {
            Id = id;
            Name = name;
            Deck = deck;
            Players = players;
            Rounds = rounds;
            ActiveRoundPlays = activeRoundPlays;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DeckDTO Deck { get; private set; }
        public RoundDTO ActiveRound { get => Rounds?.FirstOrDefault(round => round.Active); }
        public IList<PlayerDTO> Players { get; private set; }
        public IList<RoundDTO> Rounds { get; private set; }
        public IList<PlayerPlayDTO> ActiveRoundPlays { get; private set; }
    }
}
