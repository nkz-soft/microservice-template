namespace NKZSoft.Template.Common.Tests.Containers;

public static class ContainerFactory
{
    private const string Database = "template_db";
    private const string PostgresUsername = "postgres";
    private const string PostgresPassword = "postgres";

    private const string RabbitMqUsername = "rabbitmq";
    private const string RabbitMqPassword = "rabbitmq";

    public static IContainer Create<T>() where T : IContainer
    {
        var type = typeof(T);
        return type switch
        {
            not null when type.IsAssignableFrom(typeof(PostgreSqlContainer)) => CreatePostgreSql(),
            not null when type.IsAssignableFrom(typeof(RabbitMqContainer)) => CreateRabbitMq(),
            not null when type.IsAssignableFrom(typeof(RedisContainer)) => CreateRedis(),
            _ => throw new NotSupportedException($"Couldn't create a container of {nameof(T)}")
        };
    }

    private static PostgreSqlContainer CreatePostgreSql() =>
        new PostgreSqlBuilder()
            .WithUsername(PostgresUsername)
            .WithPassword(PostgresPassword)
            .WithDatabase(Database)
            .WithImage("postgres:16-alpine")
            .WithPortBinding(5432, 5432)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();

    private static RabbitMqContainer CreateRabbitMq() =>
        new RabbitMqBuilder()
            .WithUsername(RabbitMqUsername)
            .WithPassword(RabbitMqPassword)
            .WithImage("rabbitmq:3.13-management")
            .WithPortBinding(5672, 5672)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
            .Build();

    private static IContainer CreateRedis() =>
        new ContainerBuilder()
            .WithImage("redis:7.4-alpine")
            .WithPortBinding(6379, 6379)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
            .Build();
}
