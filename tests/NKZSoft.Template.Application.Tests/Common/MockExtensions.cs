namespace NKZSoft.Template.Application.Tests.Common;

public static class MockExtensions
{
    public static Mock<IMediator> SetupGetRequestObject<TFunc, TResult>(this Mock<IMediator> mock, TResult result)
        where TFunc : IRequest<TResult>
    {
        mock.Setup(mediator => mediator.Send<TResult>(It.IsAny<TFunc>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(result))
            .Verifiable();

        return mock;
    }

    public static Mock<IMediator> SetupCallHandler<TFunc, TResult>(this Mock<IMediator> mock, Func<TResult> handler)
        where TFunc : IRequest<TResult>
    {
        mock.Setup(mediator => mediator.Send<TResult>(It.IsAny<TFunc>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(handler()))
            .Verifiable();

        return mock;
    }
}
