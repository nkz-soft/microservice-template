namespace NKZSoft.Template.Common.Tests;

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    protected const string EnvironmentName = "Test";

    private  IReadOnlyDictionary<Type, ITestcontainersContainer> Containers { get; }

    protected BaseWebApplicationFactory()
    {
        TestcontainersSettings.ResourceReaperEnabled = false;
        Containers = new Dictionary<Type, ITestcontainersContainer>()
        {
            {
                typeof(PostgreSqlTestcontainer), ContainerFactory.Create<PostgreSqlTestcontainer>()
            },
            {
                typeof(RabbitMqTestcontainer), ContainerFactory.Create<RabbitMqTestcontainer>()
            }
        };

    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(Containers.Select(c => c.Value.StartAsync()));

        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<IApplicationDbContext>();
        await context.MigrateAsync();
        await context.SeedAsync();
    }

    public new async Task DisposeAsync() =>
        await Task.WhenAll(Containers.Select(c => c.Value.DisposeAsync().AsTask()));

    protected T GetContainer<T>() where T : class
    {
        if (!Containers.TryGetValue(typeof(T), out var container))
        {
            throw new ArgumentException($"Couldn't find any container of {nameof(T)}");
        }

        return (container as T)!;
    }

    protected override IHostBuilder CreateHostBuilder() =>
        base.CreateHostBuilder()
            .UseSerilog(((ctx, lc) => lc
                .WriteTo.Console()));
}
