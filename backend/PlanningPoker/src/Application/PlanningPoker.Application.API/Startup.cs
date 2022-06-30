using PlanningPoker.Infra.IoC;
using System.Reflection;

namespace PlanningPoker.Application.API
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, Assembly[] executingAssemblies)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddPlanningPoker(executingAssemblies);
        }

        public static void Configure(WebApplication app)
        {
            var env = app.Environment;
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
