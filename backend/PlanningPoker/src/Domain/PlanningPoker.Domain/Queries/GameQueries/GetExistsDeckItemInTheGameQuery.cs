using MediatR;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetExistsDeckItemInTheGameQuery : IRequest<bool>
    {
        public GetExistsDeckItemInTheGameQuery(Guid deckItemId, Guid gameId)
        {
            DeckItemId = deckItemId;
            GameId = gameId;
        }

        public Guid DeckItemId { get; private set; }
        public Guid GameId { get; private set; }
    }
}
