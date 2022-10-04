namespace NKZSoft.Template.Presentation.Starter;

using GRPC.Extensions;
using GRPC.Services;
using Microsoft.AspNetCore.Builder;
using NKZSoft.Service.Configuration.Logger;
using NKZSoft.Template.Application;
using NKZSoft.Template.Application.Common.Handlers;
using NKZSoft.Template.Infrastructure.Core;
using NKZSoft.Template.Persistence.PostgreSQL;
using NKZSoft.Template.Presentation.REST.Extensions;
using ProtoBuf.Grpc.Server;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(Configuration)
            .AddOptions()
            .AddPersistence(Configuration)
            .AddApplication()
            .AddCoreInfrastructure()
            .AddRestPresentation(Configuration, Environment)
            .AddGrpcPresentation(Configuration);
    }

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
        });
    }
}
