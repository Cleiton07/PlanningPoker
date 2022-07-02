using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Infra.Data;
using PlanningPoker.Infra.Data.Contexts;
using PlanningPoker.Infra.Data.Repositories;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Infra.IoC
{
    public static class IoC
    {
        public static void AddPlanningPoker(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfraData(configuration)
                .AddDomain()
                .AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IPlanningPokerDbContext, PlanningPokerDbContext>(options =>
                options.UseMySql(connection, ServerVersion.AutoDetect(connection)), ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDeckRepository, DeckRepository>();
            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }

        private static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<Notifications.INotification, Notifications.Notification>();
            return services;
        }
    }
}
