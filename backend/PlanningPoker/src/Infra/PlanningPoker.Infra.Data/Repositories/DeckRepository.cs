using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.DeckQueries;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories
{
    public class DeckRepository : IDeckRepository,
        IRequestHandler<GetDeckByIdQuery, Deck>,
        IRequestHandler<GetExistsDeckByIdQuery, bool>,
        IRequestHandler<GetDeckItemsDTOByDeckIdQuery, IList<DeckItemDTO>>
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

        public async Task<IList<DeckItemDTO>> Handle(GetDeckItemsDTOByDeckIdQuery request, CancellationToken cancellationToken)
            => await _context.DeckItems.AsNoTracking()
                .Where(item => item.DeckId == request.DeckId)
                .Select(item => new DeckItemDTO(item.Id, item.Value, item.Order))
                .ToListAsync(cancellationToken);
    }
}
