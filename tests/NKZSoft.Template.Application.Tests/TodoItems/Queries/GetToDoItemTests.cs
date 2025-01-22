namespace NKZSoft.Template.Application.Tests.TodoItems.Queries;

using Application.TodoItems.Queries.GetItem;
using Domain.AggregatesModel.ToDoAggregates.Entities;

[Collection("QueryCollection")]
public sealed class GetToDoItemTests : TestBase
{
    public GetToDoItemTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnItem()
    {
        var firstEntity = Context.Set<ToDoItem>().First();

        var command = new GetTodoItemQuery(firstEntity.Id);
        var result = await Mediator.Send(command, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
