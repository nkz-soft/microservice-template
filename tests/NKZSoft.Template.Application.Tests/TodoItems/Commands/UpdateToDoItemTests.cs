namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

using Application.TodoItems.Features.Commands.Create.v1;
using Application.TodoItems.Features.Commands.Update.v1;

[Collection("QueryCollection")]
public sealed class UpdateToDoItemTests : TestBase
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
        await Mediator.Send(updateCommand);
    }
}
