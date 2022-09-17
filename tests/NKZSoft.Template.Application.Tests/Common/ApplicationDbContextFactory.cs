using Microsoft.EntityFrameworkCore;
using NKZSoft.Template.Application.Common.Interfaces;
using NKZSoft.Template.Application.Tests.SeedData;
using NKZSoft.Template.Common.Tests;
using NKZSoft.Template.Infrastructure.Core.Services;
using NKZSoft.Template.Persistence.PostgreSQL;

namespace NKZSoft.Template.Application.Tests.Common;

public sealed class ApplicationDbContextFactory
{
    public static async Task<IApplicationDbContext> CreateAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(
            options,
            AppMockFactory.CreateCurrentUserServiceMock(),
            new MachineDateTime(),
            AppMockFactory.CreateMediatorMock(),
            new SeedDataContext());

        await context.Database.EnsureCreatedAsync();
        await context.SeedAsync();
        await context.SaveChangesAsync();

        return context;
    }

    public static void Destroy(IApplicationDbContext context)
    {
        context.AppDbContext.Database.EnsureDeleted();

        context.AppDbContext.Dispose();
    }
}
