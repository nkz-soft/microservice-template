namespace NKZSoft.Template.Presentation.REST.Extensions;

using Middleware;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseRestPresentation(
        this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger(configuration);

        app.UseCors("CorsPolicy");

        app.UseMiddleware(typeof(ErrorHandlingMiddleware));

        return app;
    }
}
