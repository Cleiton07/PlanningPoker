using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Infra.Data.Contexts
{
    public class PlanningPokerDbContext : DbContext, IPlanningPokerDbContext
    {
        public DbSet<Deck> Decks { get; set; }
        public DbSet<DeckItem> DeckItems { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }

        public void DicardChanges() => ChangeTracker.Clear();
    }
}
