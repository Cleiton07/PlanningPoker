using MediatR;
using PlanningPoker.Domain.Core.DTOs;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetRoundPlaysQuery : IRequest<IList<PlayerPlayDTO>>
    {
        public Guid RoundId { get; private set; }

        public GetRoundPlaysQuery(Guid roundId)
        {
            RoundId = roundId;
        }
    }
}
