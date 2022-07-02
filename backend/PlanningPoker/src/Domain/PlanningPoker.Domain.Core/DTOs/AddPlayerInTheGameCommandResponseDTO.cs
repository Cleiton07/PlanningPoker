using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.DTOs
{
    public class AddPlayerInTheGameCommandResponseDTO
    {
        public AddPlayerInTheGameCommandResponseDTO(Player player, Game game, Round activeRound,
            IList<PlayerDTO> players, IList<RoundDTO> rounds, IList<PlayerPlayDTO> activeRoundPlays)
        {
            PlayerId = player.Id;
            PlayerNickname = player.Nickname;

            GameId = game.Id;
            GameName = game.Name;

            ActiveRoundId = activeRound?.Id;
            ActiveRoundName = activeRound?.Name;
            ActiveRoundPlays = activeRoundPlays;
            Players = players;
            Rounds = rounds;
        }

        public Guid PlayerId { get; private set; }
        public string PlayerNickname { get; private set; }
        public Guid GameId { get; private set; }
        public string GameName { get; private set; }
        public Guid? ActiveRoundId { get; private set; }
        public string ActiveRoundName { get; private set; }
        public IList<PlayerDTO> Players { get; private set; }
        public IList<RoundDTO> Rounds { get; private set; }
        public IList<PlayerPlayDTO> ActiveRoundPlays { get; private set; }
    }
}
