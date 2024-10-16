namespace NKZSoft.Template.Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using NKZSoft.Template.Application.Common.Interfaces;
using SeedData;
using NKZSoft.Template.Common.Tests;
using Infrastructure.Core.Services;
using Persistence.PostgreSQL;

public static class ApplicationDbContextFactory
{
    public static async Task<IApplicationDbContext> CreateAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.InitContext(
            AppMockFactory.CreateCurrentUserServiceMock(),
            new SeedDataContext(),
            new MachineDateTime(),
            AppMockFactory.CreateMediatorMock());

        await context.Database.EnsureCreatedAsync().ConfigureAwait(false);
        await context.SeedAsync().ConfigureAwait(false);

        return context;
    }

    public static void Destroy(IApplicationDbContext context)
    {
        context.AppDbContext.Database.EnsureDeleted();

        context.AppDbContext.Dispose();
    }
}
