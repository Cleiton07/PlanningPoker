using MediatR;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetExistsActiveRoundByGameIdQuery : IRequest<bool>
    {
        public Guid GameId { get; private set; }

        public GetExistsActiveRoundByGameIdQuery(Guid gameId)
        {
            GameId = gameId;
        }
    }
}
