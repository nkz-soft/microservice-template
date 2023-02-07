using System.Reflection;
using EFCoreSecondLevelCacheInterceptor;
using NKZSoft.Template.EFCore.Caching.Redis.Extensions;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
        .AddEFCoreRedisCache(configuration)
    .AddApplication()
    .AddCoreInfrastructure()
    .AddRestPresentation(configuration, builder.Environment)
//#if (EnableGrpc)
    .AddGrpcPresentation(configuration)
//#endif
//#if (EnableGraphQL)
    .AddGraphQLPresentation()
//#endif
//#if (EnableSignalR)
    .AddSignalRPresentation()
//#endif
    .AddMessageBroker(configuration)
    .AddHealthChecks();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(builder => builder
        .AddService(
            serviceName: Assembly.GetExecutingAssembly().GetName().Name!,
            serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
            serviceInstanceId: Environment.MachineName))
    .WithTracing(builder => builder
        .AddAspNetCoreInstrumentation(options =>
        {
            options.EnableGrpcAspNetCoreSupport = true;
            options.RecordException = true;
        })
        .AddRestOpenTelemetry()
        .AddNgpSqlPersistenceOpenTelemetry()
        .AddMassTransitOpenTelemetry()
        .AddOtlpExporter()
        .AddConsoleExporter()
    )
    .StartWithHost();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await context.MigrateAsync();
        await context.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
#pragma warning disable CA1848
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
#pragma warning restore CA1848
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
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

//We need public access to the class for tests
public partial class Program {}
