namespace NKZSoft.Template.Presentation.SignalR.Extensions;

using Hubs;

public static class EndpointRouteBuilderExtension
{
    /// <summary>
    /// Extension method to map a SignalR hub endpoint.
    /// </summary>
    /// <param name="endpointRouteBuilder">The <see cref="IEndpointRouteBuilder"/> to extend.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> for further configuration.</returns>
    public static IEndpointRouteBuilder MapHubEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapHub<ToDoItemHub>("/" + nameof(ToDoItemHub));
        return endpointRouteBuilder;
    }
}
