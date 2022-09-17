using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
using NKZSoft.Template.Persistence.PostgreSQL;

namespace NKZSoft.Template.Application.Tests.SeedData;

public sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        await context.AppDbContext.Set<ToDoItem>().AddRangeAsync(ToDoItems);
        await context.SaveChangesAsync();
    }
}
