namespace NKZSoft.Template.Persistence.PostgreSQL;

using Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        configuration.ThrowIfNull(nameof(configuration));

        var currentConfiguration = configuration.GetSection(DbConfigurationSection.SectionName)
            .Get<DbConfigurationSection>();

        currentConfiguration.ThrowIfNull(nameof(currentConfiguration));
        currentConfiguration.PostgresConnection.ThrowIfNull(nameof(currentConfiguration.PostgresConnection));
        currentConfiguration.PostgresConnection?.ConnectionString.ThrowIfNull(nameof(currentConfiguration.PostgresConnection.ConnectionString));
        currentConfiguration.PostgresConnection?.Database.ThrowIfNull(nameof(currentConfiguration.PostgresConnection.Database));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql($"{currentConfiguration.PostgresConnection?.ConnectionString}" +
                              $"Database={currentConfiguration.PostgresConnection?.Database}");

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
        return services;
    }
    private static DbContextOptionsBuilder EnableDbLogging(this DbContextOptionsBuilder builder) => builder
            .LogTo(
                msg => Log.Logger.Information(msg),
                new[] { DbLoggerCategory.Database.Name })
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
}
