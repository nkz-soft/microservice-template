namespace NKZSoft.Template.Presentation.SignalR.Tests.Common;

using SeedDataContext = SeedData.SeedDataContext;

public sealed class SignalRWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment(EnvironmentName);
        builder.ConfigureServices((_, services) =>
        {
            services
                .Replace<IDbInitializer, SeedDataContext>()
                .Replace<ICurrentUserService>(p => AppMockFactory.CreateCurrentUserServiceMock())
                .ConfigureDbContextFactory(GetContainer<PostgreSqlTestcontainer>().ConnectionString);
        });
    }

    public async Task<HubConnection> CreateConnectionAsync(string controller)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl(new Uri(Server.BaseAddress, $"{controller}"), o =>
        {
            o.HttpMessageHandlerFactory = _ => Server.CreateHandler();
        })
        .Build();
        await connection.StartAsync();
        return connection;
    }
}
