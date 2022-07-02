using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.GameQueries;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories
{
    public class GameRepository : IGameRepository,
        IRequestHandler<GetExistsGameByInviteCodeQuery, bool>,
        IRequestHandler<GetGameByInviteCodeQuery, Game>
    {
        private readonly IPlanningPokerDbContext _context;

        public GameRepository(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
            => await _context.Games.AddAsync(game, cancellationToken);

        public async Task AddPlayerAsync(Player player, CancellationToken cancellationToken = default)
            => await _context.Players.AddAsync(player, cancellationToken);

        public async Task<bool> Handle(GetExistsGameByInviteCodeQuery request, CancellationToken cancellationToken)
            => await _context.Games.AnyAsync(game => game.InviteCode == request.InviteCode, cancellationToken);

        public async Task<Game> Handle(GetGameByInviteCodeQuery request, CancellationToken cancellationToken)
            => await _context.Games.FirstOrDefaultAsync(game => game.InviteCode == request.InviteCode, cancellationToken);
    }
}
