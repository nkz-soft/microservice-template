namespace NKZSoft.Template.Presentation.Rest.Extensions;

using Filters;
using Middleware;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRestPresentation(
        this IServiceCollection services, IConfiguration configuration)
    {
        var corsParams = configuration.GetSection("Cors").Get<List<string>>();

        ArgumentNullException.ThrowIfNull(corsParams);

        services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
        {
            builder.WithOrigins(corsParams.ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        services.AddHttpContextAccessor()
            .AddSwagger(configuration, Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssemblyContaining<IApplicationDbContext>(filter:null, includeInternalTypes:true)
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails()
            .AddControllers(options => options.Filters.Add<CustomExceptionFilterAttribute>())
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}
