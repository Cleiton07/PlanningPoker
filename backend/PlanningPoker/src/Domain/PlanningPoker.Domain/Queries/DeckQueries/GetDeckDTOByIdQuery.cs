using MediatR;
using PlanningPoker.Domain.Core.DTOs;

namespace PlanningPoker.Domain.Queries.DeckQueries
{
    public class GetDeckDTOByIdQuery : IRequest<DeckDTO>
    {
        public Guid DeckId { get; private set; }

        public GetDeckDTOByIdQuery(Guid deckId)
        {
            DeckId = deckId;
        }
    }
}
