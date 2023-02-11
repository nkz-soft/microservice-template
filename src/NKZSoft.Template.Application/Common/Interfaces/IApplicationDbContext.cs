namespace NKZSoft.Template.Application.Common.Interfaces;

public interface IApplicationDbContext
{
#pragma warning disable CA1716
    DbSet<T> Set<T>()
#pragma warning restore CA1716
        where T : class;

    DbContext AppDbContext { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task MigrateAsync();

    Task SeedAsync();
}
