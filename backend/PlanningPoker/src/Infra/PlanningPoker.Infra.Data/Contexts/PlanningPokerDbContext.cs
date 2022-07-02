using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Infra.Data.Contexts
{
    public class PlanningPokerDbContext : DbContext, IPlanningPokerDbContext
    {
        public DbSet<Deck> Decks { get; set; }
        public DbSet<DeckItem> DeckItems { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Round> Rounds { get; set; }

        public PlanningPokerDbContext(DbContextOptions options) : base(options)
        {

        }


        public void DicardChanges() => ChangeTracker.Clear();

        public async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class, IModel
        {
            var locals = Set<TEntity>().Where(e => e.Id == entity.Id);
            foreach (var local in locals)
                Entry(local).State = EntityState.Detached;

            Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);
        }
    }
}
