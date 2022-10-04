namespace NKZSoft.Template.Common.Tests;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NKZSoft.Template.Application.Common.Interfaces;
using Serilog;
using Xunit;

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    protected const string EnvironmentName = "Test";
    private const string Database = "template_db";
    private const string Username = "postgres";
    private const string Password = "postgres";
    public TestcontainerDatabase Container { get; }

    public BaseWebApplicationFactory()
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
