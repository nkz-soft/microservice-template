using NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Application.TodoItems.Commands.Create;

namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

[Collection("QueryCollection")]
public class CreateToDoItemTests : TestBase
{
    private const string ToDoItemTitle = "Title";

    public CreateToDoItemTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldCreateTodoItem()
    {
        var command = new CreateToDoItemCommand(ToDoItemTitle, null);
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }
}
