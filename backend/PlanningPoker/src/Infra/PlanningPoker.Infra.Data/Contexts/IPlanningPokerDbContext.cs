using Microsoft.EntityFrameworkCore;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Infra.Data.Contexts
{
    public interface IPlanningPokerDbContext
    {
        DbSet<Deck> Decks { get; set; }
        DbSet<DeckItem> DeckItems { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Player> Players { get; set; }

        void DicardChanges();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
