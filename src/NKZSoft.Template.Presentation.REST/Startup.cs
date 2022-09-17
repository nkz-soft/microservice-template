using Microsoft.AspNetCore.Builder;
using NKZSoft.Template.Application;
using NKZSoft.Template.Application.Common.Handlers;
using NKZSoft.Template.Infrastructure.Core;
using NKZSoft.Template.Persistence.PostgreSQL;
using NKZSoft.Template.Presentation.REST.Extensions;
using NKZSoft.Template.Presentation.REST.Filters;
using NKZSoft.Template.Presentation.REST.Middleware;

namespace NKZSoft.Template.Presentation.REST;

using Filters;
using Middleware;
using NKZSoft.Service.Configuration.Logger;
using NKZSoft.Service.Configuration.Swagger;

public class Startup
{
  private readonly Assembly _assemblyApplication = typeof(HandlerBase<,>).GetTypeInfo().Assembly;

  public Startup(IConfiguration configuration, IWebHostEnvironment environment)
  {
    Configuration = configuration;
    Environment = environment;
  }

  public IConfiguration Configuration { get; }

  public IWebHostEnvironment Environment { get; }

  // This method gets called by the runtime. Use this method to add services to the container.
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddLogging(Configuration)
          .AddOptions()
          .AddSwagger(Configuration, Assembly.GetExecutingAssembly())
          .AddPersistence(Configuration)
          .AddApplication()
          .AddCoreInfrastructure()
          .AddPresentation(Configuration, Environment)
          .AddControllers(options => options.Filters.Add<CustomExceptionFilterAttribute>());
  }

  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
  public void Configure(
    IApplicationBuilder app,
    IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    /*
    app.UseHealthChecks("/hc", new HealthCheckOptions
      {
        Predicate = _ => true
      })
      .UseHealthChecks("/hcex", new HealthCheckOptions
      {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });
*/

    app.UseSwagger(Configuration);

    app.UseCors("CorsPolicy");

    app.UseRouting();

//    app.UseAuthorization();
//
    app.UseMiddleware(typeof(ErrorHandlingMiddleware));

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });


  }
}
