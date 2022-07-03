using MediatR;
using PlanningPoker.Domain.Core.DTOs;

namespace PlanningPoker.Domain.Queries.DeckQueries
{
    public class GetDeckItemsDTOByDeckIdQuery : IRequest<IList<DeckItemDTO>>
    {
        public Guid DeckId { get; private set; }

        public GetDeckItemsDTOByDeckIdQuery(Guid deckId)
        {
            DeckId = deckId;
        }
    }
}
