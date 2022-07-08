namespace PlanningPoker.Domain.Builders.interfaces
{
    public interface IBaseBuilder<T> where T : class
    {
        Task<T> BuildAsync(CancellationToken cancellationToken);
    }
}
