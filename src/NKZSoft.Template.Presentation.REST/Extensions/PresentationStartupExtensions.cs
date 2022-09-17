namespace NKZSoft.Template.Presentation.REST.Extensions;

public static class PresentationStartupExtensions
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();

        var corsParams = configuration.GetSection("Cors").Get<List<string>>();
            
        services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
        {
            builder.WithOrigins(corsParams.Where(x => x != null).ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));
            
        return services;
    }
}