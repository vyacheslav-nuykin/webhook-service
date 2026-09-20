namespace WebhookService.Api.Endpoints;

public static class InfoEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/info", () => Results.Ok(new
        {
            service = "webhook-service",
            version = "0.1.0"
        }));
    }
}