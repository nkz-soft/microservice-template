namespace NKZSoft.Template.Common.Tests.Containers;

public static class ContainerFactory
{
    private const string Database = "template_db";
    private const string Username = "postgres";
    private const string Password = "postgres";

    private const string RabbitMqUsername = "rabbitmq";
    private const string RabbitMqPassword = "rabbitmq";

    public static IContainer Create<T>() where T : IContainer
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

    private static IContainer CreatePostgreSql() =>
#pragma warning disable CS0618
        new ContainerBuilder<PostgreSqlTestcontainer>()
#pragma warning restore CS0618
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

    private static IContainer CreateRabbitMq() =>
#pragma warning disable CS0618
        new ContainerBuilder<RabbitMqTestcontainer>()
#pragma warning restore CS0618
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

    private static IContainer CreateRedis() =>
        new ContainerBuilder()
            .WithImage("redis:7.0")
            .WithPortBinding(6379, 6379)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
            .Build();
}
