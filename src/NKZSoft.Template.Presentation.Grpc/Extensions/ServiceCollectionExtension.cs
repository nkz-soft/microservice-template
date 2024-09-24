namespace NKZSoft.Template.Presentation.Grpc.Extensions;

using Mapper = MapsterMapper.Mapper;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGrpcPresentation(
        this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());

        services.TryAddSingleton<IMapper>(new Mapper(typeAdapterConfig));
        services.TryAddSingleton<IMapper, ServiceMapper>();

        services.AddGrpcServer();

        return services;
    }
}
