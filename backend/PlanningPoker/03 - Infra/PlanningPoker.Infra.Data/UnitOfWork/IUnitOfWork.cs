using PlanningPoker.Infra.Data.Repositories.Interfaces;

namespace PlanningPoker.Infra.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDeckRepository DeckRepository { get; }
        IGameRepository GameRepository { get; }

        void Commit();
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
