using NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Application.TodoItems.Queries.Get;

namespace NKZSoft.Template.Application.Tests.TodoItems.Queries;

[Collection("QueryCollection")]
public class GetToDoItemTests : TestBase
{
    public GetToDoItemTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(1)]
    public async Task ShouldReturnItem(int id)
    {
        var command = new GetTodoItemQuery(id);
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
