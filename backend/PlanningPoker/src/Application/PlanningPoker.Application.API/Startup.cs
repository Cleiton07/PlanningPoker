using PlanningPoker.Infra.IoC;

namespace PlanningPoker.Application.API
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddPlanningPoker(configuration);
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
