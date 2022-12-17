namespace NKZSoft.Template.Common.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Replace<TService>(this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory) where TService : class =>
        services.TryGetService<TService>(out var serviceDescriptor)
            ? services.Replace(
                new ServiceDescriptor(typeof(TService), implementationFactory, serviceDescriptor.Lifetime))
            : services;

    public static IServiceCollection Replace<TService, TImplementation>(this IServiceCollection services) =>
        services.TryGetService<TService>(out var serviceDescriptor)
            ? services.Replace(
                new ServiceDescriptor(typeof(TService), typeof(TImplementation), serviceDescriptor.Lifetime))
            : services;

    private static bool TryGetService<TService>(this IServiceCollection services,
        [NotNullWhen(true)] out ServiceDescriptor? serviceDescriptor)
    {
        serviceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(TService));
        return serviceDescriptor != null;
    }
}
