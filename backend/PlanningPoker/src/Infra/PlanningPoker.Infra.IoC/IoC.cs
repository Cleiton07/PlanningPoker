using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanningPoker.Domain.Core.Interfaces;
using PlanningPoker.Domain.Core.Interfaces.Repositories;
using PlanningPoker.Infra.Data;
using PlanningPoker.Infra.Data.Contexts;
using PlanningPoker.Infra.Data.Repositories;
using System.Reflection;
using Notifications = PlanningPoker.Domain.Core.Notification;

namespace PlanningPoker.Infra.IoC
{
    public static class IoC
    {
        public static IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public static void AddPlanningPoker(this IServiceCollection services, Assembly[] executingAssemblies)
        {
            services
                .AddMediatR(executingAssemblies)
                .AddDomain()
                .AddInfraData();
        }

        private static IServiceCollection AddInfraData(this IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
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
