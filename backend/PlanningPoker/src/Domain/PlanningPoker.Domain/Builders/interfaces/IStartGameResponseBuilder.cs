using PlanningPoker.Domain.Core.DTOs;
using PlanningPoker.Domain.Core.Models;

namespace PlanningPoker.Domain.Builders.interfaces
{
    public interface IStartGameResponseBuilder : IBaseBuilder<StartGameResponseDTO>
    {
        IStartGameResponseBuilder WithGame(Game game);
        IStartGameResponseBuilder WithPlayer(Player player);
    }
}
