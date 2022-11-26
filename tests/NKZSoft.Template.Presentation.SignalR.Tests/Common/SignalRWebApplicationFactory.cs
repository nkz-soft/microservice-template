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
                .Remove<ApplicationDbContext>()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(GetContainer<PostgreSqlTestcontainer>().ConnectionString);
                })
                .AddScoped<IApplicationDbContext, ApplicationDbContext>()
                .AddScoped<IDbInitializer, SeedDataContext>()
                .Remove<ICurrentUserService>()
                .AddTransient(p => AppMockFactory.CreateCurrentUserServiceMock());
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
