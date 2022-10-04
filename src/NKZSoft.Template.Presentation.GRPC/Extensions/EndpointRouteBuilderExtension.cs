namespace NKZSoft.Template.Presentation.GRPC.Extensions;

using Services;

public static class EndpointRouteBuilderExtension
{
    public static IEndpointRouteBuilder MapGrpcEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGrpcService<ToDoItemService>();
        endpointRouteBuilder.MapCodeFirstGrpcReflectionService();
        return endpointRouteBuilder;
    }
}
