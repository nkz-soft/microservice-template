namespace NKZSoft.Template.Presentation.REST.Tests.SeedData;

internal sealed partial class SeedDataContext : IDbInitializer
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        await context.AppDbContext.Set<ToDoItem>().AddRangeAsync(ToDoItems);
        await context.SaveChangesAsync();
    }
}
