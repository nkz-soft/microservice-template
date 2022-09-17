using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Common.Tests;

namespace NKZSoft.Template.Application.Tests;

internal sealed class Startup
{
    public static async void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.TryAddSingleton(AppMockFactory.CreateCurrentUserServiceMock());
        services.TryAddSingleton(await ApplicationDbContextFactory.CreateAsync());
    }
}
