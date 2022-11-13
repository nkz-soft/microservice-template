namespace NKZSoft.Template.Persistence.PostgreSQL.Extensions;

using Configurations;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Add PostgresSQL as a persistence layer
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddNgpSqlPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        configuration.ThrowIfNull(nameof(configuration));

        var currentConfiguration = configuration.GetSection(DbConfigurationSection.SectionName)
            .Get<DbConfigurationSection>();

        ArgumentNullException.ThrowIfNull(currentConfiguration);
        ArgumentNullException.ThrowIfNull(currentConfiguration.PostgresConnection);
        currentConfiguration.PostgresConnection.ConnectionString.ThrowIfNull(nameof(currentConfiguration.PostgresConnection.ConnectionString));
        currentConfiguration.PostgresConnection.Database.ThrowIfNull(nameof(currentConfiguration.PostgresConnection.Database));

        var connectionString = $"{currentConfiguration.PostgresConnection.ConnectionString}" +
                               $"Database={currentConfiguration.PostgresConnection.Database}";

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);

            var hasDbLogging = configuration.GetValue<bool>("Serilog:EnableDbLogging");
            if (hasDbLogging)
            {
                options.EnableDbLogging();
            }
        });

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

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
    private static DbContextOptionsBuilder EnableDbLogging(this DbContextOptionsBuilder builder) => builder
            .LogTo(
                msg => Log.Logger.Information(msg),
                new[] { DbLoggerCategory.Database.Name })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
}
