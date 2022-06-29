using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Notifications = PlanningPoker.Domain.Notifications;

namespace PlanningPoker.Infra.IoC
{
    public static class IoC
    {
        public static void AddPlanningPoker(this IServiceCollection services, Assembly[] executingAssemblies)
        {
            services
                .AddMediatR(executingAssemblies)
                .AddDomain()
                .AddInfraData()
                .AddApplication();
        }

        private static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddInfraData(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<Notifications.INotification, Notifications.Notification>();
            return services;
        }
    }
}
