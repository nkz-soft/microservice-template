namespace NKZSoft.Template.Presentation.REST.Tests.Common;

using DotNet.Testcontainers.Containers;
using SeedData;

public class RestWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
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
}
