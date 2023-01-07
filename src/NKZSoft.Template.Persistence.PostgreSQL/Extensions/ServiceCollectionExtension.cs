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

        var currentConfiguration = configuration.GetSection(DbConfigurationSection.SectionName)
            .Get<DbConfigurationSection>();

        ArgumentNullException.ThrowIfNull(currentConfiguration);
        ArgumentNullException.ThrowIfNull(currentConfiguration.PostgresConnection);
        currentConfiguration.PostgresConnection.ConnectionString.ThrowIfNull(nameof(currentConfiguration.PostgresConnection.ConnectionString));

        var connectionString = currentConfiguration.PostgresConnection.ConnectionString;

        ConfigureDbContextFactory(services, connectionString,
            currentConfiguration.PostgresConnection.LoggingEnabled, optionsBuilder);

        services.TryAddScoped<IDbInitializer, DbInitializer>();
        services.TryAddScoped<ApplicationDbContextFactory>();

        services.TryAddScoped<IApplicationDbContext>(p =>
            p.GetRequiredService<ApplicationDbContextFactory>().CreateDbContext());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(RepositoryBase<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        if (currentConfiguration.PostgresConnection.HealthCheckEnabled)
        {
            services.AddHealthChecks().AddNpgSql(connectionString);
        }

        return services;
    }

    public static IServiceCollection ConfigureDbContextFactory(this IServiceCollection services,
        string? connectionString,
        bool enableDbLogging = true,
        Action<IServiceProvider, DbContextOptionsBuilder>? optionsBuilder = null) =>
        services.AddEntityFrameworkNpgsql()
            .AddPooledDbContextFactory<ApplicationDbContext>(optionsAction: (provider, options) =>
            {
                optionsBuilder?.Invoke(provider, options);

                options.UseInternalServiceProvider(provider);
                options.UseNpgsql(connectionString);

                if (enableDbLogging)
                {
                    options.EnableDbLogging();
                }
            });

    private static DbContextOptionsBuilder EnableDbLogging(this DbContextOptionsBuilder builder) => builder
            .LogTo(
                msg => Log.Logger.Information(msg),
                new[] { DbLoggerCategory.Database.Name })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
}
