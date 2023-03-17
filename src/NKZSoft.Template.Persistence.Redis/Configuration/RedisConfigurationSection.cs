namespace NKZSoft.Template.Persistence.Redis.Configuration;

internal sealed record RedisConfigurationSection
{
    public const string ProviderName = "RedisStorage";

    public const string SectionName = "ConnectionStrings:RedisCacheConnection";

    public RedisConnection? RedisConnection { get; init; }
}
