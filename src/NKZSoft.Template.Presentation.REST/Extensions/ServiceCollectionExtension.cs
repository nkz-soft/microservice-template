namespace NKZSoft.Template.Presentation.REST.Extensions;

using Filters;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRestPresentation(
        this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var corsParams = configuration.GetSection("Cors").Get<List<string>>();

        services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
        {
            builder.WithOrigins(corsParams.ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        services.AddHttpContextAccessor()
            .AddSwagger(configuration, Assembly.GetExecutingAssembly())
            .AddControllers(options => options.Filters.Add<CustomExceptionFilterAttribute>())
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}
