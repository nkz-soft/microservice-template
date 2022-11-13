namespace NKZSoft.Template.Application.Tests.TodoItems.Commands;

[Collection("QueryCollection")]
public sealed class CreateToDoItemTests : TestBase
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
