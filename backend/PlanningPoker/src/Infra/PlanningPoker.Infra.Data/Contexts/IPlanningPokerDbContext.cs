using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Infra.Data.Contexts
{
    public interface IPlanningPokerDbContext
    {
        DbSet<Deck> Decks { get; set; }
        DbSet<DeckItem> DeckItems { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Play> Plays { get; set; }
        DbSet<Round> Rounds { get; set; }

        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class, IModel;

        void DicardChanges();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
