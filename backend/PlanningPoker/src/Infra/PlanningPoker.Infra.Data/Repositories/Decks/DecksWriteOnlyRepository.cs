using PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories.Decks
{
    public class DecksWriteOnlyRepository : IDecksWriteOnlyRepository
    {
        private readonly IPlanningPokerDbContext _context;

        public DecksWriteOnlyRepository(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Deck deck, CancellationToken cancellationToken = default)
            => await _context.Decks.AddAsync(deck, cancellationToken);
    }
}
