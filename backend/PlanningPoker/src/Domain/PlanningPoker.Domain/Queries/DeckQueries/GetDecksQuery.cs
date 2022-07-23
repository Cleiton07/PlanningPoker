using MediatR;
using PlanningPoker.Domain.Core.DTOs;

namespace PlanningPoker.Domain.Queries.DeckQueries
{
    public class GetDecksQuery : IRequest<GetDecksQueryResponseDTO>
    {
        public GetDecksQuery(string search, int page)
        {
            Search = search;
            Page = page > 0 ? page : 1;
        }

        public string Search { get; private set; }
        public int Page { get; private set; }
        public static int PageSize => 20;
    }
}
