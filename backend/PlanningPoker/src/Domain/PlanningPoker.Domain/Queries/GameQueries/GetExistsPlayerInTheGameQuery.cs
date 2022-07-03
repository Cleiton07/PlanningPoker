using MediatR;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetExistsPlayerInTheGameQuery : IRequest<bool>
    {
        public GetExistsPlayerInTheGameQuery(Guid playerId, Guid gameId)
        {
            PlayerId = playerId;
            GameId = gameId;
        }

        public Guid PlayerId { get; private set; }
        public Guid GameId { get; private set; }
    }
}
