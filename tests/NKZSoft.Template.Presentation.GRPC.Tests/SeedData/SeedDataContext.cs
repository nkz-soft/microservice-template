namespace NKZSoft.Template.Presentation.GRPC.Tests.SeedData;

internal sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<ToDoItem>().AddRangeAsync(SeedDataContext.ToDoItems, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
