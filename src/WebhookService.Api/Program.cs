using WebhookService.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

HealthEndpoints.Map(app);
InfoEndpoints.Map(app);

app.Run();