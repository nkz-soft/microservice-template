namespace NKZSoft.Template.Presentation.GraphQL.Tests.Common;

using Microsoft.Extensions.Options;
using Persistence.PostgreSQL.Configuration;
using SeedData;
using Testcontainers.PostgreSql;

public sealed class GraphQLWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
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
                .Replace<IOptions<PostgresConnection>>(p =>
                    Options.Create(new PostgresConnection()
                    {
                        ConnectionString = GetContainer<PostgreSqlContainer>().GetConnectionString(),
                        HealthCheckEnabled = false,
                        LoggingEnabled = true
                    }));
        });
    }
}
