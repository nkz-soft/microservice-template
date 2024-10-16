namespace NKZSoft.Template.Application.Tests.SeedData;
using Domain.AggregatesModel.ToDoAggregates.Entities;

using Application.Common.Interfaces;

public sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await context.AppDbContext.Set<ToDoItem>()
            .AddRangeAsync(ToDoItems, cancellationToken)
            .ConfigureAwait(false);
        await context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
