namespace NKZSoft.Template.Persistence.PostgreSQL;

public class DbInitializer : IDbInitializer
{
    public Task SeedAsync(ApplicationDbContext context)
    {
        return Task.CompletedTask;
    }
}