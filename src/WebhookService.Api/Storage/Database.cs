using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace WebhookService.Api.Storage;

public sealed class Database : IHealthCheck
{
    private static readonly TimeSpan HealthCheckTimeout = TimeSpan.FromSeconds(2);

    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public NpgsqlConnection OpenConnection()
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        return conn;
    }

    public async Task PingAsync(CancellationToken cancellationToken = default)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync(cancellationToken).ConfigureAwait(false);

        await using var cmd = new NpgsqlCommand("SELECT 1;", conn);
        await cmd.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
    HealthCheckContext context,
    CancellationToken cancellationToken = default)
    {
        using (var probe = new NpgsqlConnection(_connectionString))
        {
            NpgsqlConnection.ClearPool(probe);
        }

        using var timeoutCts = new CancellationTokenSource(HealthCheckTimeout);
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken, timeoutCts.Token);

        try
        {
            await PingAsync(linkedCts.Token).ConfigureAwait(false);
            return HealthCheckResult.Healthy("Database is OK");
        }
        catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested
                                                 && !cancellationToken.IsCancellationRequested)
        {
            return HealthCheckResult.Unhealthy(
                $"Database health check timed out after {HealthCheckTimeout.TotalSeconds} seconds.");
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Database is unavailable: {ex.Message}");
        }
    }
}