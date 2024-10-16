namespace NKZSoft.Template.Application.Tests;
using NKZSoft.Template.Common.Tests;

internal static class Startup
{
#pragma warning disable VSTHRD100
    public static async void ConfigureServices(IServiceCollection services)
#pragma warning restore VSTHRD100
    {
        services.AddApplication();
        services.TryAddSingleton(AppMockFactory.CreateCurrentUserServiceMock());
        services.TryAddSingleton(await ApplicationDbContextFactory.CreateAsync().ConfigureAwait(false));

        services.TryAddScoped<IToDoItemRepository, ToDoItemRepository>();
        services.TryAddScoped<IToDoListRepository, ToDoListRepository>();
    }

}
