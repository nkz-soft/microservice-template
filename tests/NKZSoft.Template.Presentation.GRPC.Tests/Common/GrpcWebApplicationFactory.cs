namespace NKZSoft.Template.Presentation.GRPC.Tests.Common;

using DotNet.Testcontainers.Containers;
using SeedData;

public sealed class GrpcWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
{
    public T CreateGrpcService<T>() where T : class
    {
        var client = CreateClient();
        var grpcChannel =  GrpcChannel.ForAddress(client.BaseAddress!, new GrpcChannelOptions
        {
            HttpClient = client
        });
        return grpcChannel.CreateGrpcService<T>();
    }

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
