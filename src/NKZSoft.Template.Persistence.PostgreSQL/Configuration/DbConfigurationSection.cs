namespace NKZSoft.Template.Persistence.PostgreSQL.Configuration;

internal sealed record DbConfigurationSection
{
    public const string SectionName = "ConnectionStrings:PostgresConnection";

    public DbConfigurationSection()
    {}

    public DbConfigurationSection(PostgresConnection postgresConnection) => PostgresConnection = postgresConnection;

    public PostgresConnection? PostgresConnection { get; init; }
}
