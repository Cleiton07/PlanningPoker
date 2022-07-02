using MediatR;

namespace PlanningPoker.Domain.Queries.GameQueries
{
    public class GetExistsGameByInviteCodeQuery : IRequest<bool>
    {
        public string InviteCode { get; private set; }

        public GetExistsGameByInviteCodeQuery(string inviteCode)
        {
            InviteCode = inviteCode;
        }
    }
}
