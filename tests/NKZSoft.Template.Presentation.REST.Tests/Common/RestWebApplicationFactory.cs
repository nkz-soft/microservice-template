namespace NKZSoft.Template.Presentation.REST.Tests.Common;

using SeedData;

public sealed class RestWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
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
}
