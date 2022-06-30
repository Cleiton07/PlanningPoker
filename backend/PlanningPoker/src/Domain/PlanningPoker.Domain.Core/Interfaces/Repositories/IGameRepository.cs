using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task<Guid> AddAsync(Game game, CancellationToken cancellationToken = default);
    }
}
