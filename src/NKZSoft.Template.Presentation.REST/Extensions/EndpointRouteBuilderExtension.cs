namespace NKZSoft.Template.Presentation.REST.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

public static class EndpointRouteBuilderExtension
{
    public static IEndpointRouteBuilder MapRestEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllers();
        return endpointRouteBuilder;
    }
}
