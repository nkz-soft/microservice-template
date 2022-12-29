namespace NKZSoft.Template.EFCore.Caching.Redis.Configuration;

internal sealed record RedisConnection
{
    [DisallowNull]
    public string? ConnectionString { get; init; }

    public bool HealthCheckEnabled { get; set; }
}
