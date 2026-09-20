namespace WebhookService.Api.Endpoints;

public static class HealthEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/health", () => Results.Ok(new { status = "ok" }));
    }
}