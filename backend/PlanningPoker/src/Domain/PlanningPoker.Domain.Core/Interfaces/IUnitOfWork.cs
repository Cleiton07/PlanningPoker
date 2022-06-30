namespace PlanningPoker.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken);
        void Rollback();
    }
}
