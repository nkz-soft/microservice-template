using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using NKZSoft.Template.Application.Common.Interfaces;
using NKZSoft.Template.Common.Tests;

namespace NKZSoft.Template.Presentation.REST.Tests.Common;

using Persistence.PostgreSQL;
using SeedData;

public sealed class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    private const string EnvironmentName = "Test";
    private const string Database = "template_db";
    private const string Username = "postgres";
    private const string Password = "postgres";
    public TestcontainerDatabase Container { get; }

    public CustomWebApplicationFactory()
    {
        TestcontainersSettings.ResourceReaperEnabled = false;
        Container = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = Database,
                Username = Username,
                Password = Password
            })
            .WithImage("postgres:14")
            .WithName("postgres")
            .WithPortBinding(5432, 5432)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();
    }

    protected override IHostBuilder CreateHostBuilder() =>
        base.CreateHostBuilder()
            .UseSerilog(((ctx, lc) => lc
                .WriteTo.Console()));

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnvironmentName);
        builder.ConfigureServices((_, services) =>
        {
            services
                .Remove<IApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>()
                .AddScoped<IApplicationDbContext, ApplicationDbContext>()
                .AddScoped<IDbInitializer, SeedDataContext>()
                .Remove<ICurrentUserService>()
                .AddTransient(p => AppMockFactory.CreateCurrentUserServiceMock());
        });
    }

    public async Task InitializeAsync()
    {
        await Container.StartAsync();

        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<IApplicationDbContext>();
        await context.MigrateAsync();
        await context.SeedAsync();
    }

    public new async Task DisposeAsync() => await Container.DisposeAsync();
}
