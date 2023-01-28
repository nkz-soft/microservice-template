namespace NKZSoft.Template.Persistence.PostgreSQL.Configuration;

internal sealed class PostgresConnectionValidator : AbstractValidator<PostgresConnection>
{
    public PostgresConnectionValidator()
    {
        RuleFor(x => x.ConnectionString).NotEmpty();
    }
}
