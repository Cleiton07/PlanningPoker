using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task AddAsync(Game game, CancellationToken cancellationToken = default);
        Task AddPlayerAsync(Player player, CancellationToken cancellationToken = default);
        Task AddRoundAsync(Round round, CancellationToken cancellationToken = default);
        Task UpdateRoundAsync(Round round, CancellationToken cancellationToken = default);
    }
}
