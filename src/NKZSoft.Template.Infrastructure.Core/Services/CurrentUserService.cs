namespace NKZSoft.Template.Infrastructure.Core.Services;

using NKZSoft.Template.Common.Extensions;

public sealed class CurrentUserService(ICurrentUser currentUser) : ICurrentUserService
{
    public ICurrentUser CurrentUser { get; } = currentUser.ThrowIfNull();
}
