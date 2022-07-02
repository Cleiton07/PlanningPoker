using MediatR;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetExistsGameByIdQuery : IRequest<bool>
    {
        public Guid GameId { get; private set; }

        public GetExistsGameByIdQuery(Guid gameId)
        {
            GameId = gameId;
        }
    }
}
