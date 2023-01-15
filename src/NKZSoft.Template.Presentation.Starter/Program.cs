using EFCoreSecondLevelCacheInterceptor;
using NKZSoft.Template.EFCore.Caching.Redis.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
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

app.MapHealthChecks("/healthz");

app.MapHealthChecks("/healthz-ex", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

//We need public access to the class for tests
public partial class Program {}
