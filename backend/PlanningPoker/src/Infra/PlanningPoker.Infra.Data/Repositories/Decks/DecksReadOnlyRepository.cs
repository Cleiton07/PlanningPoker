using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.DeckQueries;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories.Decks
{
    public class DecksReadOnlyRepository :
        IRequestHandler<GetDeckByIdQuery, Deck>,
        IRequestHandler<GetExistsDeckByIdQuery, bool>,
        IRequestHandler<GetExistsDeckByNameQuery, bool>,
        IRequestHandler<GetDeckItemsDTOByDeckIdQuery, IList<DeckItemDTO>>,
        IRequestHandler<GetDecksQuery, GetDecksQueryResponseDTO>,
        IRequestHandler<GetDeckDTOByIdQuery, DeckDTO>
    {
        private readonly IPlanningPokerDbContext _context;

        public DecksReadOnlyRepository(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public async Task<Deck> Handle(GetDeckByIdQuery request, CancellationToken cancellationToken)
            => await _context.Decks.AsNoTracking().FirstOrDefaultAsync(deck => deck.Id == request.DeckId, cancellationToken);

        public async Task<bool> Handle(GetExistsDeckByIdQuery request, CancellationToken cancellationToken)
            => await _context.Decks.AsNoTracking().AnyAsync(deck => deck.Id == request.DeckId, cancellationToken);

        public async Task<bool> Handle(GetExistsDeckByNameQuery request, CancellationToken cancellationToken)
            => await _context.Decks.AsNoTracking().AnyAsync(deck => deck.Name == request.DeckName.Trim(), cancellationToken);

        public async Task<IList<DeckItemDTO>> Handle(GetDeckItemsDTOByDeckIdQuery request, CancellationToken cancellationToken)
            => await _context.DeckItems.AsNoTracking()
                .Where(item => item.DeckId == request.DeckId)
                .Select(item => new DeckItemDTO(item.Id, item.Value, item.Order))
                .ToListAsync(cancellationToken);

        public async Task<GetDecksQueryResponseDTO> Handle(GetDecksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Decks.AsNoTracking().Include(deck => deck.Items).Select(deck => deck);

            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(deck => EF.Functions.Like(deck.Name, $"%{request.Search.Trim()}%"));

            int total = await query.CountAsync(cancellationToken);

            query = query.Skip(request.Page * GetDecksQuery.PageSize).Take(GetDecksQuery.PageSize);
            var list = (await query.ToListAsync(cancellationToken))
                .Select(deck => new DeckDTO(
                    deck.Id,
                    deck.Name,
                    deck.Items?.Select(item => new DeckItemDTO(item.Id, item.Value, item.Order))?.ToList()
                )).ToList();

            return new(list, total, request.Search, request.Page, GetDecksQuery.PageSize);
        }

        public async Task<DeckDTO> Handle(GetDeckDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var deck = await _context.Decks.AsNoTracking()
                .Include(deck => deck.Items)
                .FirstOrDefaultAsync(deck => deck.Id == request.DeckId, cancellationToken);

            if (deck is null) return null;

            return new(
                deck.Id,
                deck.Name,
                deck.Items?.Select(item => new DeckItemDTO(item.Id, item.Value, item.Order))?.ToList()
            );
        }
    }
}
