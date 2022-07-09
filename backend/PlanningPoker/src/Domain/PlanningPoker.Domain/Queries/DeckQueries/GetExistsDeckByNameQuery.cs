using MediatR;

namespace PlanningPoker.Domain.Queries.DeckQueries
{
    public class GetExistsDeckByNameQuery : IRequest<bool>
    {
        public string DeckName { get; private set; }

        public GetExistsDeckByNameQuery(string deckName)
        {
            DeckName = deckName;
        }
    }
}
