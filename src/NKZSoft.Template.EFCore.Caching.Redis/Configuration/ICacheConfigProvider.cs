namespace NKZSoft.Template.EFCore.Caching.Redis.Configuration;

internal interface ICacheConfigProvider
{
    CacheConfigurationSection GetConfig();
}
