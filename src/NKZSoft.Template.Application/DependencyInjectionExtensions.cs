﻿namespace NKZSoft.Template.Application;

using Common.Behaviours;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.ThrowIfNull();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), filter: null, includeInternalTypes: true);
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());

        services.TryAddSingleton<IMapper>(new MapsterMapper.Mapper(typeAdapterConfig));
        services.TryAddSingleton<IMapper, ServiceMapper>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));

        return services;
    }
}
