namespace NKZSoft.Template.EFCore.Caching.Redis.Configuration;

internal sealed record CacheConfigurationSection
{
    public const string SectionName = "ConnectionStrings";

    public RedisConnection? RedisConnection { get; init; }
}
