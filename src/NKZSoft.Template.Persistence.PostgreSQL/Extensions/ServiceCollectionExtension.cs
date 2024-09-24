namespace NKZSoft.Template.Persistence.PostgreSQL.Extensions;

using Configuration;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Add PostgresSQL as a persistence layer
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddNgpSqlPersistence(this IServiceCollection services,
        IConfiguration configuration,
        Action<IServiceProvider, DbContextOptionsBuilder>? optionsBuilder = null)
    {
        configuration.ThrowIfNull(nameof(configuration));

        services.AddWithValidation<PostgresConnection, PostgresConnectionValidator>(
            configuration.GetSection(DbConfigurationSection.SectionName));

        services.AddScoped<IValidator<PostgresConnection>, PostgresConnectionValidator>();

        ConfigureDbContextFactory(services,optionsBuilder);

        services.TryAddScoped<IDbInitializer, DbInitializer>();
        services.TryAddScoped<ApplicationDbContextFactory>();

        services.TryAddScoped<IApplicationDbContext>(p =>
            p.GetRequiredService<ApplicationDbContextFactory>().CreateDbContext());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(RepositoryBase<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }

    private static IServiceCollection ConfigureDbContextFactory(this IServiceCollection services,
        Action<IServiceProvider, DbContextOptionsBuilder>? optionsBuilder = null)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var config = serviceProvider.GetRequiredService<IOptions<PostgresConnection>>();

        services.AddEntityFrameworkNpgsql()
            .AddPooledDbContextFactory<ApplicationDbContext>(optionsAction: (provider, options) =>
            {
                optionsBuilder?.Invoke(provider, options);

                options.UseInternalServiceProvider(provider);
                options.UseNpgsql(config.Value.ConnectionString);

                if (config.Value.LoggingEnabled)
                {
                    options.EnableDbLogging();
                }
            });

        if (config.Value.HealthCheckEnabled)
        {
            services.AddHealthChecks().AddNpgSql(config.Value.ConnectionString);
        }

        return services;
    }

    private static DbContextOptionsBuilder EnableDbLogging(this DbContextOptionsBuilder builder) => builder
            .LogTo(
                msg => Log.Logger.Information(msg),
                new[] { DbLoggerCategory.Database.Name })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
}
