using Microsoft.EntityFrameworkCore;
using NKZSoft.Template.Application.Common.Interfaces;
using NKZSoft.Template.Application.Tests.SeedData;
using NKZSoft.Template.Common.Tests;
using NKZSoft.Template.Infrastructure.Core.Services;
using NKZSoft.Template.Persistence.PostgreSQL;

namespace NKZSoft.Template.Application.Tests.Common;

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

        await context.Database.EnsureCreatedAsync();
        await context.SeedAsync();

        return context;
    }

    public static void Destroy(IApplicationDbContext context)
    {
        context.AppDbContext.Database.EnsureDeleted();

        context.AppDbContext.Dispose();
    }
}
