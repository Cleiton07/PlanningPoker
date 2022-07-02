using MediatR;
using PlanningPoker.Domain.Core.DTOs;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetGameRoundsQuery : IRequest<IList<RoundDTO>>
    {
        public GetGameRoundsQuery(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; private set; }
    }
}
