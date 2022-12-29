namespace NKZSoft.Template.Persistence.PostgreSQL.Configuration;

public sealed record PostgresConnection
{
    public string ConnectionString { get; init; } = default!;

    public bool HealthCheckEnabled { get; init; } = true;

    public bool LoggingEnabled { get; init; }
}
