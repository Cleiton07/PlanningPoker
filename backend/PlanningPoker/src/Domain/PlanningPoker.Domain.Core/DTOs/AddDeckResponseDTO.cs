namespace PlanningPoker.Domain.Core.DTOs
{
    public class AddDeckResponseDTO
    {
        public AddDeckResponseDTO(Guid deckId, string deckName, IList<DeckItemDTO> deckItems)
        {
            DeckId = deckId;
            DeckName = deckName;
            DeckItems = deckItems;
        }

        public Guid DeckId { get; private set; }
        public string DeckName { get; private set; }
        public IList<DeckItemDTO> DeckItems { get; private set; }
    }
}
