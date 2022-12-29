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
            },
            {
                typeof(RedisTestcontainer), ContainerFactory.Create<RedisTestcontainer>()
            }
        };
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(Containers.Select(c => c.Value.StartAsync()));

        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<IApplicationDbContext>();

        //The local machine may still have old volumes
        await context.AppDbContext.Database.EnsureDeletedAsync();

        await context.MigrateAsync();
        await context.SeedAsync();
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await Task.WhenAll(Containers.Select(c => c.Value.DisposeAsync().AsTask()));
    }

    protected T GetContainer<T>() where T : class
    {
        if (!Containers.TryGetValue(typeof(T), out var container))
        {
            throw new ArgumentException($"Couldn't find any container of {nameof(T)}");
        }

        return (container as T)!;
    }
}
