namespace NKZSoft.Template.Persistence.PostgreSQL;

public interface IDbInitializer
{
    Task SeedAsync(ApplicationDbContext context);
}