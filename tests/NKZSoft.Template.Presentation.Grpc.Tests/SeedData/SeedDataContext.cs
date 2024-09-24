namespace NKZSoft.Template.Presentation.Grpc.Tests.SeedData;

internal sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<ToDoItem>()
            .AddRangeAsync(SeedDataContext.ToDoItems, cancellationToken)
            .ConfigureAwait(false);
        await context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
