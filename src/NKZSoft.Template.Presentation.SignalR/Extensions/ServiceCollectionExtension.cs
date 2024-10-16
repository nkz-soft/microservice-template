namespace NKZSoft.Template.Presentation.SignalR.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds SignalR presentation services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection with SignalR presentation services added.</returns>
    public static IServiceCollection AddSignalRPresentation(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}
