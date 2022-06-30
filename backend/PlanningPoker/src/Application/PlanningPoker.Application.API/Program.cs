using PlanningPoker.Application.API;

var builder = WebApplication.CreateBuilder(args);
Startup.ConfigureServices(builder.Services, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
Startup.Configure(app);

app.Run();