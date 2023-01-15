namespace NKZSoft.Template.Presentation.Grpc.Tests.Common;

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
                .Replace<IDbInitializer, SeedDataContext>()
                .Replace<ICurrentUserService>(p => AppMockFactory.CreateCurrentUserServiceMock())
                .ConfigureDbContextFactory(GetContainer<PostgreSqlTestcontainer>().ConnectionString);
        });
    }
}
