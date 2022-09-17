namespace NKZSoft.Template.Persistence.PostgreSQL.Configurations;

public class DbConfigurationSection
{
    public const string SectionName = "ConnectionStrings";

    public DbConfigurationSection()
    {}

    public DbConfigurationSection(PostgresConnection postgresConnection) => PostgresConnection = postgresConnection;

    public PostgresConnection? PostgresConnection { get; init; }
}
