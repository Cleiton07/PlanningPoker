using MediatR;

namespace PlanningPoker.Domain.Queries.DeckQueries
{
    public class GetExistsDeckByIdQuery : IRequest<bool>
    {
        public Guid DeckId { get; private set; }

        public GetExistsDeckByIdQuery(Guid deckId)
        {
            DeckId = deckId;
        }
    }
}
