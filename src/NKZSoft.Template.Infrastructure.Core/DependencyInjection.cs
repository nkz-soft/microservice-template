using Microsoft.Extensions.DependencyInjection;
using NKZSoft.Template.Application.Common.Interfaces;
using NKZSoft.Template.Infrastructure.Core.Services;

namespace NKZSoft.Template.Infrastructure.Core;

using Services;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTime, MachineDateTime>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}
