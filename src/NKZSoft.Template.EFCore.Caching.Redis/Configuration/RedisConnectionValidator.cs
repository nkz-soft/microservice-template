namespace NKZSoft.Template.EFCore.Caching.Redis.Configuration;

internal sealed class RedisConnectionValidator : AbstractValidator<RedisConnection>
{
    public RedisConnectionValidator()
    {
        RuleFor(x => x.ConnectionString).NotEmpty();
    }
}
