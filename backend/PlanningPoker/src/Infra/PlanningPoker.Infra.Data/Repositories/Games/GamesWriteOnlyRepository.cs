using PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories.Games
{
    public class GamesWriteOnlyRepository : IGamesWriteOnlyRepository
    {
        private readonly IPlanningPokerDbContext _context;

        public GamesWriteOnlyRepository(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
            => await _context.Games.AddAsync(game, cancellationToken);

        public async Task AddPlayAsync(Play play, CancellationToken cancellationToken = default)
            => await _context.Plays.AddAsync(play, cancellationToken);

        public async Task AddPlayerAsync(Player player, CancellationToken cancellationToken = default)
            => await _context.Players.AddAsync(player, cancellationToken);

        public async Task AddRoundAsync(Round round, CancellationToken cancellationToken = default)
            => await _context.Rounds.AddAsync(round, cancellationToken);

        public async Task UpdateRoundAsync(Round round, CancellationToken cancellationToken = default)
            => await _context.UpdateAsync(round, cancellationToken);
    }
}
