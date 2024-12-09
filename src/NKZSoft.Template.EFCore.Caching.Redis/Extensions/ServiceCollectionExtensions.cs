namespace NKZSoft.Template.EFCore.Caching.Redis.Extensions;

using Configuration;
using NKZSoft.Template.Common.Extensions;

public static class ServiceCollectionExtensions
{
    private const string ProviderName = "EFCoreCahce";
    private const string SerializerName = "proto";

    private const string EfPrefix = "EF_";

    /// <summary>
    /// Adds EFCore second level caching
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddEfCoreRedisCache(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.ThrowIfNull();
        configuration.ThrowIfNull();

        services.AddWithValidation<RedisConnection, RedisConnectionValidator>(
            configuration.GetSection(CacheConfigurationSection.SectionName));

        services.AddEFSecondLevelCache(options =>
            options.UseEasyCachingCoreProvider(ProviderName, isHybridCache: false)
                .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30))
                .ConfigureLogging(enable: true)
                .UseCacheKeyPrefix(EfPrefix)
        );

        services.AddEasyCaching(option =>
        {
            using var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<RedisConnection>>();

            option.WithJson(SerializerName);
            option.UseRedis(config =>
            {
                config.DBConfig.ConfigurationOptions
                    = ConfigurationOptions.Parse(options.Value.ConnectionString);
                config.SerializerName = SerializerName;
            }, ProviderName);

            if (options.Value.HealthCheckEnabled)
            {
                services.AddHealthChecks().AddRedis(options.Value.ConnectionString!, ProviderName);
            }
        });
        return services;
    }
}
