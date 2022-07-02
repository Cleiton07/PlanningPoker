using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.DeckQueries;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories
{
    public class DeckRepository : IDeckRepository,
        IRequestHandler<GetDeckByIdQuery, Deck>,
        IRequestHandler<GetExistsDeckByIdQuery, bool>
    {
        private readonly IPlanningPokerDbContext _context;

        public DeckRepository(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public async Task<Deck> Handle(GetDeckByIdQuery request, CancellationToken cancellationToken)
            => await _context.Decks.AsNoTracking().FirstOrDefaultAsync(deck => deck.Id == request.DeckId, cancellationToken);

        public async Task<bool> Handle(GetExistsDeckByIdQuery request, CancellationToken cancellationToken)
            => await _context.Decks.AsNoTracking().AnyAsync(deck => deck.Id == request.DeckId, cancellationToken);
    }
}
