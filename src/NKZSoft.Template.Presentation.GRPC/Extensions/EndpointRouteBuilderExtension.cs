namespace NKZSoft.Template.Presentation.GRPC.Extensions;

using Services;

public static class EndpointRouteBuilderExtension
{
    public static IEndpointRouteBuilder MapGrpcEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGrpcEndpoints<ToDoItemService>();
        return endpointRouteBuilder;
    }
}
