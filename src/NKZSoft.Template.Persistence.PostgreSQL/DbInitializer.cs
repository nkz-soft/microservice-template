namespace NKZSoft.Template.Persistence.PostgreSQL;

using Application.Common.Interfaces;

public class DbInitializer : IDbInitializer
{
    public Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
