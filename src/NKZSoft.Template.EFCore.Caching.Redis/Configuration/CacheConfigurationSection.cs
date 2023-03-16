namespace NKZSoft.Template.EFCore.Caching.Redis.Configuration;

internal sealed record CacheConfigurationSection
{
    public const string SectionName = "ConnectionStrings:RedisCacheConnection";

    public RedisConnection? RedisConnection { get; init; }
}
