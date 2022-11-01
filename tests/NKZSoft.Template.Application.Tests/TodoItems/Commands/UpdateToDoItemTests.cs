using NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Application.TodoItems.Commands.Create;
using NKZSoft.Template.Application.TodoItems.Commands.Update;

namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

[Collection("QueryCollection")]
public class UpdateToDoItemTests : TestBase
{
    private const string ToDoItemTitle = "UpdateTitle";
    private const string ToDoItemUpdatedTitle = "UpdatedTitle";

    public UpdateToDoItemTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldUpdateTodoItem()
    {
        var createCommand = new CreateToDoItemCommand(ToDoItemTitle, null);
        var createResult = await Mediator.Send(createCommand);

        createResult.Should().NotBeNull();
        createResult.IsSuccess.Should().BeTrue();

        var updateCommand = new UpdateTodoItemCommand(createResult.Value, ToDoItemUpdatedTitle, ToDoItemUpdatedTitle);
        var updateResult = await Mediator.Send(updateCommand);
    }
}
