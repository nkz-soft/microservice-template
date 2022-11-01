using NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Application.TodoItems.Commands.Create;
using NKZSoft.Template.Application.TodoItems.Commands.Delete;

namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

[Collection("QueryCollection")]
public class DeleteToDoItemTests : TestBase
{
    private const string ToDoItemTitle = "DeleteTitle";
    public DeleteToDoItemTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldDeleteTodoItem()
    {
        var createCommand = new CreateToDoItemCommand(ToDoItemTitle, null);
        var createResult = await Mediator.Send(createCommand);

        createResult.Should().NotBeNull();
        createResult.IsSuccess.Should().BeTrue();

        var deleteCommand = new DeleteTodoItemCommand(createResult.Value);
        var deleteResult = await Mediator.Send(deleteCommand);
    }
}
