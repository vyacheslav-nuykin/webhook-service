using WebhookService.Api.Storage;

namespace WebhookService.Api.Endpoints;

public static class HealthEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/health", async (Database db, CancellationToken ct) =>
        {
            try
            {
                await db.PingAsync(ct);
                return Results.Ok(new { status = "ok", database = "ok" });
            }
            catch
            {
                return Results.Json(
                    new { status = "degraded", database = "unreachable" },
                    statusCode: StatusCodes.Status503ServiceUnavailable);
            }
        });
    }
}