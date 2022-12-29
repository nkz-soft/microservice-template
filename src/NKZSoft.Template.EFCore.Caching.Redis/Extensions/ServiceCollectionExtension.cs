namespace NKZSoft.Template.EFCore.Caching.Redis.Extensions;

using Common;
using Configuration;

public static class ServiceCollectionExtension
{
    private const string EfPrefix = "EF_";
    private const string SerializerName = "proto";

    /// <summary>
    /// Adds EFCore second level caching
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddEFCoreRedisCache(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.ThrowIfNull();
        configuration.ThrowIfNull();

        var cacheConfigProvider = CacheConfigProvider.Create(configuration);

        var cacheConfigurationSection = cacheConfigProvider.GetConfig();

        //TODO We need a smarter validation here
        ArgumentNullException.ThrowIfNull(cacheConfigurationSection.RedisConnection);
        ArgumentNullException.ThrowIfNull(cacheConfigurationSection.RedisConnection.ConnectionString);

        services.AddEFSecondLevelCache(options =>
            options.UseEasyCachingCoreProvider(SerializerName, isHybridCache: false)
                .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30))
                .DisableLogging(false)
                .UseCacheKeyPrefix(EfPrefix)
        );

        services.AddEasyCaching(option =>
        {
            option.WithJson(SerializerName);
            option.UseCSRedis(config =>
            {
                config.DBConfig = new CSRedisDBOptions
                {
                    ConnectionStrings = new List<string>(1)
                    {
                        cacheConfigurationSection.RedisConnection.ConnectionString
                    }
                };
            }, SerializerName);
        });

        if (cacheConfigurationSection.RedisConnection.HealthCheckEnabled)
        {
            services.AddHealthChecks().AddRedis(cacheConfigurationSection.RedisConnection.ConnectionString);
        }

        return services;
    }
}
