namespace NKZSoft.Template.Persistence.PostgreSQL.Configuration;

internal sealed class PostgresConnectionValidator : AbstractValidator<PostgresConnection>
{
    public PostgresConnectionValidator()
    {
        RuleFor(connection => connection.ConnectionString).NotEmpty();
    }
}
