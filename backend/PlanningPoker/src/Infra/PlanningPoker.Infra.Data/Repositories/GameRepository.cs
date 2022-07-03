using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.GameQueries;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data.Repositories
{
    public class GameRepository : IGameRepository,
        IRequestHandler<GetExistsGameByInviteCodeQuery, bool>,
        IRequestHandler<GetExistsGameByIdQuery, bool>,
        IRequestHandler<GetExistsPlayerInTheGameQuery, bool>,
        IRequestHandler<GetExistsDeckItemInTheGameQuery, bool>,
        IRequestHandler<GetExistsActiveRoundByGameIdQuery, bool>,
        IRequestHandler<GetGameByInviteCodeQuery, Game>,
        IRequestHandler<GetActiveRoundQuery, Round>,
        IRequestHandler<GetRoundPlaysQuery, IList<PlayerPlayDTO>>,
        IRequestHandler<GetGamePlayersQuery, IList<PlayerDTO>>,
        IRequestHandler<GetGameRoundsQuery, IList<RoundDTO>>
    {
        private readonly IPlanningPokerDbContext _context;

        public GameRepository(IPlanningPokerDbContext context)
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

        public async Task<bool> Handle(GetExistsGameByIdQuery request, CancellationToken cancellationToken)
            => await _context.Games.AsNoTracking().AnyAsync(game => game.Id == request.GameId, cancellationToken);

        public async Task<bool> Handle(GetExistsGameByInviteCodeQuery request, CancellationToken cancellationToken)
            => await _context.Games.AsNoTracking().AnyAsync(game => game.InviteCode == request.InviteCode, cancellationToken);

        public async Task<bool> Handle(GetExistsPlayerInTheGameQuery request, CancellationToken cancellationToken)
            => await _context.Players.AsNoTracking().AnyAsync(player => player.Id == request.PlayerId && player.GameId == request.GameId, cancellationToken);

        public async Task<bool> Handle(GetExistsDeckItemInTheGameQuery request, CancellationToken cancellationToken)
        {
            return await (
                from deckItem in _context.DeckItems.AsNoTracking()
                join deck in _context.Decks.AsNoTracking() on deckItem.DeckId equals deck.Id
                join game in _context.Games.AsNoTracking() on deck.Id equals game.DeckId
                where deckItem.Id == request.DeckItemId && game.Id == request.GameId
                select new { deckItem, deck, game }
            ).AnyAsync(cancellationToken);
        }

        public async Task<bool> Handle(GetExistsActiveRoundByGameIdQuery request, CancellationToken cancellationToken)
            => await _context.Rounds.AsNoTracking().AnyAsync(round => round.GameId == request.GameId && round.Active, cancellationToken);

        public async Task<Game> Handle(GetGameByInviteCodeQuery request, CancellationToken cancellationToken)
            => await _context.Games.AsNoTracking().FirstOrDefaultAsync(game => game.InviteCode == request.InviteCode, cancellationToken);

        public async Task<Round> Handle(GetActiveRoundQuery request, CancellationToken cancellationToken)
            => await _context.Rounds.AsNoTracking().FirstOrDefaultAsync(round => round.GameId == request.GameId && round.Active, cancellationToken);

        public async Task<IList<PlayerPlayDTO>> Handle(GetRoundPlaysQuery request, CancellationToken cancellationToken)
        {
            var query = from play in _context.Plays.AsNoTracking()
                        join deckItem in _context.DeckItems.AsNoTracking() on play.DeckItemId equals deckItem.Id
                        join round in _context.Rounds.AsNoTracking() on play.RoundId equals round.Id
                        join player in _context.Players.AsNoTracking() on play.PlayerId equals player.Id
                        where round.Id == request.RoundId
                        select new PlayerPlayDTO(round.Id, round.Name, player.Id, player.Nickname,
                            deckItem.Id, deckItem.Value, play.DateTimeOfPlay);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IList<PlayerDTO>> Handle(GetGamePlayersQuery request, CancellationToken cancellationToken)
            => await _context.Players.AsNoTracking()
                .Where(player => player.GameId == request.GameId)
                .Select(player => new PlayerDTO(player.Id, player.Nickname, player.Excluded))
                .ToListAsync(cancellationToken);

        public async Task<IList<RoundDTO>> Handle(GetGameRoundsQuery request, CancellationToken cancellationToken)
            => await _context.Rounds.AsNoTracking()
                .Where(round => round.GameId == request.GameId)
                .Select(round => new RoundDTO(round.Id, round.Name, round.Active))
                .ToListAsync(cancellationToken);

        public async Task UpdateRoundAsync(Round round, CancellationToken cancellationToken = default)
            => await _context.UpdateAsync(round, cancellationToken);
    }
}
