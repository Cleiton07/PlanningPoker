using PlanningPoker.Domain.Entities;

namespace PlanningPoker.Infra.Data.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Guid> AddAsync(Game game, CancellationToken cancellationToken = default);
    }
}
