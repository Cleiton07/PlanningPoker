using MediatR;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetGameByInviteCodeQuery : IRequest<Game>
    {
        public string InviteCode { get; private set; }

        public GetGameByInviteCodeQuery(string inviteCode)
        {
            InviteCode = inviteCode;
        }
    }
}
