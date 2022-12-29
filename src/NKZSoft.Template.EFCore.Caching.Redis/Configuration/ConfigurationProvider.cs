namespace NKZSoft.Template.EFCore.Caching.Redis.Configuration;

using Common;

internal sealed class CacheConfigProvider : ICacheConfigProvider
{
    private readonly IConfiguration _configuration;

    private CacheConfigProvider(IConfiguration configuration) =>
        _configuration = configuration.ThrowIfNull();

    public CacheConfigurationSection GetConfig() =>
        _configuration.GetSection(CacheConfigurationSection.SectionName)
            .Get<CacheConfigurationSection>();

    public static ICacheConfigProvider Create(IConfiguration configuration) => new CacheConfigProvider(configuration);
}
