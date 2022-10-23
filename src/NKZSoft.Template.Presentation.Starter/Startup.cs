namespace NKZSoft.Template.Presentation.Starter;

using GraphQL.Extensions;
using GRPC.Extensions;
using MessageBrokers.RabbitMq.Extensions;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services) =>
        services.AddLogging(Configuration)
            .AddOptions()
            .AddPersistence(Configuration)
            .AddApplication()
            .AddCoreInfrastructure()
            //TODO Scan assemblies and map all presenters through one interface.
            .AddRestPresentation(Configuration, Environment)
            .AddGrpcPresentation(Configuration)
            .AddGraphQLPresentation()
            .AddMessageBroker(Configuration);

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRestPresentation(Configuration, env);

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            //TODO Scan assemblies and map all endpoints through one interface.
            endpoints.MapRestEndpoints();
            endpoints.MapGrpcEndpoints();
            endpoints.MapGraphQLEndpoints();
        });
    }
}
