using MediatR;
using PlanningPoker.Domain.Builders.interfaces;
using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Models;
using PlanningPoker.Domain.Queries.DeckQueries;
using PlanningPoker.Domain.Queries.GameQueries;

namespace PlanningPoker.Domain.Builders
{
    public class StartGameResponseBuilder : IStartGameResponseBuilder
    {
        private readonly IMediator _mediator;

        public StartGameResponseBuilder(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Game Game { get; set; }
        private Player Player { get; set; }

        public IStartGameResponseBuilder WithGame(Game game)
        {
            Game = game;
            return this;
        }

        public IStartGameResponseBuilder WithPlayer(Player player)
        {
            Player = player;
            return this;
        }

        public async Task<StartGameResponseDTO> BuildAsync(CancellationToken cancellationToken)
        {
            var deck = await _mediator.Send(new GetDeckByIdQuery(Game.DeckId), cancellationToken);
            var deckItems = await _mediator.Send(new GetDeckItemsDTOByDeckIdQuery(Game.DeckId), cancellationToken);
            var players = await _mediator.Send(new GetGamePlayersQuery(Game.Id), cancellationToken);
            var rounds = await _mediator.Send(new GetGameRoundsQuery(Game.Id), cancellationToken);
            var activeRound = await _mediator.Send(new GetActiveRoundQuery(Game.Id), cancellationToken);
            var activeRoundPlays = activeRound is null
                ? null
                : await _mediator.Send(new GetRoundPlaysQuery(activeRound.Id), cancellationToken);

            return new(Player, Game, deck, deckItems, players, rounds, activeRoundPlays);
        }
    }
}
