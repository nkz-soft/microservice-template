namespace NKZSoft.Template.Persistence.Redis.Extensions;

using Common;
using Configuration;
using Repositories;

public static class ServiceCollectionExtension
{
    private const string SerializerName = "proto";

    /// <summary>
    /// Add Redis as a persistence layer
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ThrowIfNull(nameof(services));
        configuration.ThrowIfNull(nameof(configuration));

        services.AddWithValidation<RedisConnection, RedisConnectionValidator>(
            configuration.GetSection(RedisConfigurationSection.SectionName));

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
            }, RedisConfigurationSection.ProviderName);

            if (options.Value.HealthCheckEnabled)
            {
                services.AddHealthChecks().AddRedis(options.Value.ConnectionString,
                    RedisConfigurationSection.ProviderName);
            }
        });

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(IRedisRepository)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}
