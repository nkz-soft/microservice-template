namespace NKZSoft.Template.Persistence.Redis.Repositories;

using Configuration;

public class ToDoListRedisRepository : IToDoItemRedisRepository, IRedisRepository
{
    private readonly IEasyCachingProviderFactory _factory;
    private readonly IEasyCachingSerializer _serializer;

    public ToDoListRedisRepository(IEasyCachingProviderFactory factory,
        IEasyCachingSerializer serializer)
    {
        _factory = factory.ThrowIfNull(nameof(factory));
        _serializer = serializer.ThrowIfNull(nameof(_serializer));
    }

    public async Task AddAsync(ToDoItem entity, CancellationToken cancellationToken = default)
    {
        entity.ThrowIfNull(nameof(entity));

        var provider = _factory.GetCachingProvider(RedisConfigurationSection.ProviderName);

        var redisDatabase = provider.Database as IDatabase;
        redisDatabase.ThrowIfNull(nameof(redisDatabase));

        await redisDatabase!.StringSetAsync(entity.Id.ToString(),
            _serializer.Serialize(entity))
            .ConfigureAwait(false);
    }

    public async Task DeleteAsync(ToDoItem entity, CancellationToken cancellationToken = default)
    {
        entity.ThrowIfNull(nameof(entity));

        var provider = _factory.GetCachingProvider(RedisConfigurationSection.ProviderName);
        await provider.RemoveAsync(entity.Id.ToString(), cancellationToken).ConfigureAwait(false);
    }

    public async Task<ToDoItem?> GetAsyncByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var provider = _factory.GetCachingProvider(RedisConfigurationSection.ProviderName);
        var cacheValue = await provider.GetAsync<ToDoItem>(id.ToString(), cancellationToken)
            .ConfigureAwait(false);
        return cacheValue.HasValue ?  cacheValue.Value : null;
    }

    public async Task DeleteAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var provider = _factory.GetCachingProvider(RedisConfigurationSection.ProviderName);
        await provider.RemoveAllAsync(ids.Select(guid => guid.ToString()), cancellationToken)
            .ConfigureAwait(false);
    }
}
