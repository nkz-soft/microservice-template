namespace NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Application.Common.Interfaces;
using SeedData;

public class TestBase
{
    protected IApplicationDbContext Context { get; init; }

    protected ICurrentUserService CurrentUserService { get;init; }

    protected SeedDataContext SeedDataContext { get; init; }

    protected IMediator Mediator { get; init; }

    protected TestBase(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mediator = fixture.Mediator;
        CurrentUserService = fixture.CurrentUserService;
        SeedDataContext = fixture.SeedDataContext;
    }
}
