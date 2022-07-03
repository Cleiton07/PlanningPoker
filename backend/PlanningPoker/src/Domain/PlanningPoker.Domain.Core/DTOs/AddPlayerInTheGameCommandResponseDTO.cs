using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.DTOs
{
    public class AddPlayerInTheGameCommandResponseDTO
    {
        public AddPlayerInTheGameCommandResponseDTO(Player player, Game game, Deck deck,
            IList<DeckItemDTO> deckItems, IList<PlayerDTO> players, IList<RoundDTO> rounds, IList<PlayerPlayDTO> activeRoundPlays)
        {
            PlayerId = player.Id;
            PlayerNickname = player.Nickname;

            Game = new(
                game.Id,
                game.Name,
                new(deck.Id, deck.Name, deckItems),
                players,
                rounds,
                activeRoundPlays
            );
        }

        public Guid PlayerId { get; private set; }
        public string PlayerNickname { get; private set; }
        public GameDTO Game { get; private set; }
    }
}
