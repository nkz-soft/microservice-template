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

        services.AddWithValidation<RedisConnection, RedisConnectionValidator>(
            configuration.GetSection(CacheConfigurationSection.SectionName));

        services.AddEFSecondLevelCache(options =>
            options.UseEasyCachingCoreProvider(SerializerName, isHybridCache: false)
                .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30))
                .DisableLogging(false)
                .UseCacheKeyPrefix(EfPrefix)
        );

        services.AddEasyCaching(option =>
        {
            using var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<RedisConnection>>();

            option.WithJson(SerializerName);
            option.UseCSRedis(config =>
            {
                config.DBConfig = new CSRedisDBOptions
                {
                    ConnectionStrings = new List<string>(1)
                    {
                        options.Value.ConnectionString!
                    }
                };
            }, SerializerName);

            if (options.Value.HealthCheckEnabled)
            {
                services.AddHealthChecks().AddRedis(options.Value.ConnectionString!);
            }
        });
        return services;
    }
}
