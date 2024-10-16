namespace NKZSoft.Template.Presentation.Grpc.Tests.Common;

using Microsoft.Extensions.Options;
using Persistence.PostgreSQL.Configuration;
using SeedData;
using Testcontainers.PostgreSql;

public sealed class GrpcWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
{
    public T CreateGrpcService<T>() where T : class
    {
        var client = CreateClient();
        var grpcChannel = GrpcChannel.ForAddress(client.BaseAddress!, new GrpcChannelOptions
        {
            HttpClient = client,
        });
        return grpcChannel.CreateGrpcService<T>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment(EnvironmentName)
            .ConfigureServices((_, services) => services
                .Replace<IDbInitializer, SeedDataContext>()
                .Replace(_ => AppMockFactory.CreateCurrentUserServiceMock())
                .Replace(_ =>
                    Options.Create(new PostgresConnection()
                    {
                        ConnectionString = GetContainer<PostgreSqlContainer>().GetConnectionString(),
                        HealthCheckEnabled = false,
                        LoggingEnabled = true,
                    })));
    }
}
