namespace NKZSoft.Template.Common.Tests;

public static class AppMockFactory
{
    private static readonly Lazy<MockRepository> MockRepository = new Lazy<MockRepository>(() => new MockRepository(MockBehavior.Default));

    private static MockRepository MockRepositoryInstance => MockRepository.Value;

    public static ICurrentUserService CreateCurrentUserServiceMock()
    {
        ICurrentUser currentUser = new CurrentUser
            { Id = 1,
                FirstName = "test",
                LastName = "test",
                MiddleName = "test",
            };

        return MockRepositoryInstance
            .Of<ICurrentUserService>()
            .First(currentUserService => currentUserService.CurrentUser == currentUser);
    }

    public static IMediator CreateMediatorMock()
    {
        var mediator = MockRepositoryInstance.Create<IMediator>();
        mediator.Setup(mockMediator => mockMediator.Publish(It.IsAny<object>(),
                           It.IsAny<CancellationToken>()))
            .Verifiable(string.Empty);

        return MockRepositoryInstance.Of<IMediator>().First();
    }
}
