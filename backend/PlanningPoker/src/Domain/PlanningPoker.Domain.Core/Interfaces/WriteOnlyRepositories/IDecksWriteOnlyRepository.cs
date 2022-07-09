using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Core.Interfaces.WriteOnlyRepositories
{
    public interface IDecksWriteOnlyRepository
    {
        Task AddAsync(Deck deck, CancellationToken cancellationToken = default);
    }
}
