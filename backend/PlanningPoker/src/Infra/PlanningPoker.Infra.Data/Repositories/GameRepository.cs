using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IPlanningPokerDbContext _context;

        public GameRepository(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Game game, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Games.AddAsync(game, cancellationToken);
            return entity.Entity.Id;
        }
    }
}
