using MediatR;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetActiveRoundQuery : IRequest<Round>
    {
        public Guid GameId { get; private set; }

        public GetActiveRoundQuery(Guid gameId)
        {
            GameId = gameId;
        }
    }
}
