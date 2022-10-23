namespace NKZSoft.Template.Infrastructure.Core.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(ICurrentUser currentUser) => CurrentUser = currentUser.ThrowIfNull();

    public ICurrentUser CurrentUser { get; }
}
