using NKZSoft.Template.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
    .AddLogging(configuration)
    .AddOptions()
    .AddNgpSqlPersistence(configuration, (provider, optionsBuilder)
        => optionsBuilder.AddInterceptors(provider.GetRequiredService<SecondLevelCacheInterceptor>()))
        .AddEfCoreRedisCache(configuration)
    .AddApplication()
    .AddCoreInfrastructure()
    .AddRestPresentation(configuration)
//#if (EnableGrpc)
    .AddGrpcPresentation()
//#endif
//#if (EnableGraphQL)
    .AddGraphQLPresentation()
//#endif
//#if (EnableSignalR)
    .AddSignalRPresentation()
//#endif
//#if (EnableRedisStorage)
    .AddRedisPersistence(configuration)
//#endif
    .AddMessageBroker(configuration)
    .AddHealthChecks();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resourceBuilder => resourceBuilder
        .AddService(
            serviceName: Assembly.GetExecutingAssembly().GetName().Name!,
            serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
            serviceInstanceId: Environment.MachineName))
    .WithTracing(trackerBuilder => trackerBuilder
        .AddAspNetCoreInstrumentation(options => options.RecordException = true)
        .AddRestOpenTelemetry()
        .AddNgpSqlPersistenceOpenTelemetry()
        .AddMassTransitOpenTelemetry()
        .AddOtlpExporter()
        .AddConsoleExporter()
    );

var app = builder.Build();

var scope = app.Services.CreateAsyncScope();
await using (scope.ConfigureAwait(false))
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await context.MigrateAsync().ConfigureAwait(false);
        await context.SeedAsync().ConfigureAwait(false);
    }
    catch (Exception exception)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.MigrationError(exception);
    }
}

app.UseRestPresentation(configuration, environment)
    .UseRouting();

app.UseAuthorization();

app.MapRestEndpoints();
//#if (EnableGrpc)
app.MapGrpcEndpoints();
//#endif
//#if (EnableGraphQL)
app.MapGraphQLEndpoints();
//#endif
//#if (EnableSignalR)
app.MapHubEndpoints();
//#endif

app.MapHealthChecks("/health/startup");
app.MapHealthChecks("/healthz", new HealthCheckOptions { Predicate = _ => false });
app.MapHealthChecks("/ready", new HealthCheckOptions { Predicate = _ => false });

app.MapHealthChecks("/health/info", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

await app.RunAsync().ConfigureAwait(false);

//We need public access to the class for tests
#pragma warning disable CS1591
public partial class Program;
#pragma warning restore CS1591
