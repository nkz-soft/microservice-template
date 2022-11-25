namespace NKZSoft.Template.Presentation.SignalR.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSignalRPresentation(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}
