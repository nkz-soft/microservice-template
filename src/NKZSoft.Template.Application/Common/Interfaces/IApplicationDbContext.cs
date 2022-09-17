namespace NKZSoft.Template.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<T> Set<T>()
        where T : class;

    DbContext AppDbContext { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task MigrateAsync();

    Task SeedAsync();
}
