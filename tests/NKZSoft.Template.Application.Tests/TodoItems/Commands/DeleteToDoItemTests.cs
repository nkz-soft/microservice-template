namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

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
        var createCommand = new CreateToDoItemCommand(ToDoItemTitle, ListId:null);
        var createResult = await Mediator.Send(createCommand);

        createResult.Should().NotBeNull();
        createResult.IsSuccess.Should().BeTrue();

        var deleteCommand = new DeleteToDoItemCommand(createResult.Value);
        await Mediator.Send(deleteCommand);
    }
}
