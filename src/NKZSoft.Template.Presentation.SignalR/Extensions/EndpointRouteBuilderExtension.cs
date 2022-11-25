namespace NKZSoft.Template.Presentation.SignalR.Extensions;

using Hubs;

public static class EndpointRouteBuilderExtension
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="endpointRouteBuilder"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHubEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapHub<ToDoItemHub>("/" + nameof(ToDoItemHub));
        return endpointRouteBuilder;
    }
}
