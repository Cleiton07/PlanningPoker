using PlanningPoker.Domain.Entities;

namespace PlanningPoker.Domain.Interfaces.Repositories
{
    public interface IDeckRepository
    {
        Task<Deck> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
