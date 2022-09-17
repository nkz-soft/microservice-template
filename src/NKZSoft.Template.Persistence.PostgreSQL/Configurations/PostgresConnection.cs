namespace NKZSoft.Template.Persistence.PostgreSQL.Configurations;

using System.Diagnostics.CodeAnalysis;

public sealed record PostgresConnection
{
    [NotNull]
    public string? ConnectionString { get; init; }

    [NotNull]
    public string? Database { get; init; }
}
