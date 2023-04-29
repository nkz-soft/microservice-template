namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

using Application.TodoItems.Features.Commands.Create.v1;
using Application.TodoItems.Features.Commands.Delete.v1;

[Collection("QueryCollection")]
public sealed class DeleteToDoItemTests : TestBase
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
        await Mediator.Send(deleteCommand);
    }
}
