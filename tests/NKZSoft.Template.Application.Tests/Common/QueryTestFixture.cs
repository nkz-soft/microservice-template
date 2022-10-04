using NKZSoft.Template.Application.Common.Interfaces;
using NKZSoft.Template.Application.Tests.SeedData;
using NKZSoft.Template.Common;
using NKZSoft.Template.Common.Tests;

namespace NKZSoft.Template.Application.Tests.Common;

public sealed class QueryTestFixture : IDisposable
{
    public IMediator Mediator { get; }

    public IApplicationDbContext Context { get; }

    public ICurrentUserService CurrentUserService { get; }

    public SeedDataContext SeedDataContext { get; set; } = new SeedDataContext();

    public QueryTestFixture(IApplicationDbContext context, ICurrentUserService currentUserService, IMediator mediator)
    {
        Context = context.ThrowIfNull();
        Mediator = mediator.ThrowIfNull();
        CurrentUserService = currentUserService.ThrowIfNull();
    }

    public void Dispose() => ApplicationDbContextFactory.Destroy(Context);
}
