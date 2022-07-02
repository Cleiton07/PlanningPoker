using MediatR;
using PlanningPoker.Domain.Core.DTOs;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetGamePlayersQuery : IRequest<IList<PlayerDTO>>
    {
        public GetGamePlayersQuery(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; private set; }
    }
}
