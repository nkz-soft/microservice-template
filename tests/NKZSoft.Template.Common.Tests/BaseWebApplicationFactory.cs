namespace NKZSoft.Template.Common.Tests;

using Testcontainers;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    protected const string EnvironmentName = "Test";

    private  IReadOnlyDictionary<Type, IContainer> Containers { get; }

    protected BaseWebApplicationFactory()
    {
        TestcontainersSettings.ResourceReaperEnabled = false;

        Containers = new Dictionary<Type, IContainer>()
        {
            {
                typeof(PostgreSqlContainer ), ContainerFactory.Create<PostgreSqlContainer >()
            },
            {
                typeof(RabbitMqContainer), ContainerFactory.Create<RabbitMqContainer>()
            },
            {
                typeof(RedisContainer), ContainerFactory.Create<RedisContainer>()
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
