using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
using NKZSoft.Template.Persistence.PostgreSQL;

namespace NKZSoft.Template.Application.Tests.SeedData;

using Application.Common.Interfaces;

public sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken)
    {
        await context.AppDbContext.Set<ToDoItem>().AddRangeAsync(ToDoItems, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
