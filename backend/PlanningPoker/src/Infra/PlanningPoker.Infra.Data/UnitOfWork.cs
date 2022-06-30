using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Infra.Data.Contexts;

namespace PlanningPoker.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPlanningPokerDbContext _context;

        public UnitOfWork(IPlanningPokerDbContext context)
        {
            _context = context;
        }

        public void Commit() => _context.SaveChanges();

        public Task CommitAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

        public void Rollback() => _context.DicardChanges();

        public void Dispose() => Rollback();
    }
}
