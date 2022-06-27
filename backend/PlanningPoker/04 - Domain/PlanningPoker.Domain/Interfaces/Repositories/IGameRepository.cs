using PlanningPoker.Domain.Entities;

namespace PlanningPoker.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<Guid> AddAsync(Game game, CancellationToken cancellationToken = default);
    }
}
