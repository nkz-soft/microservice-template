﻿namespace NKZSoft.Template.EFCore.Caching.Redis.Extensions;

using Common;
using Configuration;

public static class ServiceCollectionExtension
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
    public static IServiceCollection AddEFCoreRedisCache(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.ThrowIfNull();
        configuration.ThrowIfNull();

        services.AddWithValidation<RedisConnection, RedisConnectionValidator>(
            configuration.GetSection(CacheConfigurationSection.SectionName));

        services.AddEFSecondLevelCache(options =>
            options.UseEasyCachingCoreProvider(ProviderName, isHybridCache: false)
                .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(30))
                .DisableLogging(false)
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
