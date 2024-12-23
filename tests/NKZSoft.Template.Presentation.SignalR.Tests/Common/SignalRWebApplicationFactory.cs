﻿namespace NKZSoft.Template.Presentation.SignalR.Tests.Common;

using Microsoft.Extensions.Options;
using Persistence.PostgreSQL.Configuration;
using Testcontainers.PostgreSql;
using SeedDataContext = SeedData.SeedDataContext;

public sealed class SignalRWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
{
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

    public async Task<HubConnection> CreateConnectionAsync(string controller)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl(new Uri(Server.BaseAddress, $"{controller}"),
                options => options.HttpMessageHandlerFactory = _ => Server.CreateHandler())
        .Build();
        await connection.StartAsync()
            .ConfigureAwait(false);
        return connection;
    }
}
