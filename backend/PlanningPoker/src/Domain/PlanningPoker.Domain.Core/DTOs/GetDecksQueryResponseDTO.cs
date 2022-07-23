namespace PlanningPoker.Domain.Core.DTOs
{
    public class GetDecksQueryResponseDTO
    {
        public GetDecksQueryResponseDTO(IList<DeckDTO> decks, int total, string search, int page, int pageSize)
        {
            Decks = decks;
            Total = total;
            Search = search;
            Page = page;
            PageSize = pageSize;
        }

        public IList<DeckDTO> Decks { get; private set; }
        public string Search { get; private set; }
        public int Total { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public bool IsLastPage { get => Page * PageSize >= Total; }
    }
}
