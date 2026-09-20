using WebhookService.Api.Endpoints;
using WebhookService.Api.Storage;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("ConnectionStrings:Default is not configured");

builder.Services.AddSingleton(new Database(connectionString));

var app = builder.Build();

HealthEndpoints.Map(app);
InfoEndpoints.Map(app);

app.Run();