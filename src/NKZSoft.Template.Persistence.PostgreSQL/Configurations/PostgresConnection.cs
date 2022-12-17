namespace NKZSoft.Template.Persistence.PostgreSQL.Configurations;

public sealed record PostgresConnection
{
    [NotNull]
    public string? ConnectionString { get; init; }

    [NotNull]
    public string? Database { get; init; }

    public bool HealthCheckEnabled { get; init; } = true;

    public bool LoggingEnabled { get; init; } = false;
}
