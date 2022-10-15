namespace NKZSoft.Template.Presentation.GraphQL.Extensions;

public static class EndpointRouteBuilderExtension
{
    /// <summary>
    /// Adds a GraphQL endpoint to the endpoint configurations.
    /// </summary>
    /// <param name="endpointRouteBuilder">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapGraphQLEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGraphQL();
        return endpointRouteBuilder;
    }
}
