using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NKZSoft.Template.Persistence.PostgreSQL.Extensions;

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
    .AddNgpSqlPersistence(configuration)
    .AddApplication()
    .AddCoreInfrastructure()
    .AddRestPresentation(configuration, builder.Environment)
    .AddGrpcPresentation(configuration)
    .AddGraphQLPresentation()
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
app.MapGrpcEndpoints();
app.MapGraphQLEndpoints();
app.MapHealthChecks("/healthz");

app.MapHealthChecks("/healthz-ex", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

//We need public access to the class for tests
public partial class Program {}
