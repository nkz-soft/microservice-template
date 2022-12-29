namespace NKZSoft.Template.Common.Tests.Containers;

public static class ContainerFactory
{
    private const string Database = "template_db";
    private const string Username = "postgres";
    private const string Password = "postgres";

    private const string RabbitMqUsername = "rabbitmq";
    private const string RabbitMqPassword = "rabbitmq";

    public static ITestcontainersContainer Create<T>() where T : ITestcontainersContainer
    {
        var type = typeof(T);
        return type switch
        {
            not null when type.IsAssignableFrom(typeof(PostgreSqlTestcontainer)) => CreatePostgreSql(),
            not null when type.IsAssignableFrom(typeof(RabbitMqTestcontainer)) => CreateRabbitMq(),
            not null when type.IsAssignableFrom(typeof(RedisTestcontainer)) => CreateRedis(),
            _ => throw new ArgumentException($"Couldn't create a container of {nameof(T)}")
        };
    }

    private static ITestcontainersContainer CreatePostgreSql() =>
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = Database,
            Username = Username,
            Password = Password
        })
        .WithImage("postgres:14")
            .WithPortBinding(5432, 5432)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();

    private static ITestcontainersContainer CreateRabbitMq() =>
        new TestcontainersBuilder<RabbitMqTestcontainer>()
            .WithMessageBroker(new RabbitMqTestcontainerConfiguration()
            {
                Username = RabbitMqUsername,
                Password = RabbitMqPassword
            })
            .WithImage("rabbitmq:3.11-management")
            .WithPortBinding(5672, 5672)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
            .Build();

    private static ITestcontainersContainer CreateRedis() =>
        new TestcontainersBuilder<RedisTestcontainer>()
            .WithImage("redis:7.0")
            .WithPortBinding(6379, 6379)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
            .Build();
}
