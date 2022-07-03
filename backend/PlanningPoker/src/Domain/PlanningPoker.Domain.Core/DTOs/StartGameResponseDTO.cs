using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.DTOs
{
    public class StartGameResponseDTO
    {
        public StartGameResponseDTO(Player player, Game game, Deck deck,
            IList<DeckItemDTO> deckItems, IList<PlayerDTO> players, IList<RoundDTO> rounds, IList<PlayerPlayDTO> activeRoundPlays)
        {
            Player = new(player.Id, player.Nickname, player.Excluded);
            Game = new(
                game.Id,
                game.Name,
                new(deck.Id, deck.Name, deckItems),
                players,
                rounds,
                activeRoundPlays
            );
        }

        public GameDTO Game { get; private set; }
        public PlayerDTO Player { get; private set; }
    }
}
