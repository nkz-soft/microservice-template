namespace NKZSoft.Template.Common.Tests;

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    protected const string EnvironmentName = "Test";

    private  FrozenDictionary<Type, IContainer> Containers { get; }

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
            },
        }.ToFrozenDictionary();
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll
            (Containers.Select(pair => pair.Value.StartAsync()))
                .ConfigureAwait(false);

        var scope = Services.CreateAsyncScope();
        await using (scope.ConfigureAwait(false))
        {
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<IApplicationDbContext>();

            //The local machine may still have old volumes
            await context.AppDbContext.Database.EnsureDeletedAsync().ConfigureAwait(false);

            await context.MigrateAsync().ConfigureAwait(false);
            await context.SeedAsync().ConfigureAwait(false);
        }
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync().ConfigureAwait(false);
        await Task.WhenAll(Containers.Select(pair => pair.Value.DisposeAsync().AsTask()))
            .ConfigureAwait(false);
    }

    protected T GetContainer<T>() where T : class
    {
        return !Containers.TryGetValue(typeof(T), out var container) ?
                   throw new NotSupportedException($"Couldn't find any container of {nameof(T)}") :
                   (container as T)!;
    }
}
