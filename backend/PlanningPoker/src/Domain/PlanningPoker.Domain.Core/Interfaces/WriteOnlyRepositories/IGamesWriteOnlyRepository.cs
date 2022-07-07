using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories
{
    public interface IGamesWriteOnlyRepository
    {
        Task AddAsync(Game game, CancellationToken cancellationToken = default);
        Task AddPlayerAsync(Player player, CancellationToken cancellationToken = default);
        Task AddRoundAsync(Round round, CancellationToken cancellationToken = default);
        Task AddPlayAsync(Play play, CancellationToken cancellationToken = default);
        Task UpdateRoundAsync(Round round, CancellationToken cancellationToken = default);
    }
}
