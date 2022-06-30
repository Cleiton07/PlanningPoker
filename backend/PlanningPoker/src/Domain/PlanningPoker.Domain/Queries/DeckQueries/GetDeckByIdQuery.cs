using MediatR;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Queries.DeckQueries
{
    public class GetDeckByIdQuery : IRequest<Deck>
    {
        public Guid DeckId { get; private set; }

        public GetDeckByIdQuery(Guid deckId)
        {
            DeckId = deckId;
        }
    }
}
