using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task AddAsync(Game game, CancellationToken cancellationToken = default);
        Task AddPlayerAsync(Player player, CancellationToken cancellationToken = default);
    }
}
