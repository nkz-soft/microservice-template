using NKZSoft.Template.Application.Common.Paging;
using NKZSoft.Template.Application.Tests.Common;
using NKZSoft.Template.Application.TodoItems.Models;
using NKZSoft.Template.Application.TodoItems.Queries.GetPage;

namespace NKZSoft.Template.Application.Tests.TodoItems.Queries;

[Collection("QueryCollection")]
public class GetPageTodoTests : TestBase
{
    private const string TestItemTitle = "TestItem_1";

    public GetPageTodoTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(1, 3)]
    public async Task ShouldReturnItemPage(int pageIndex, int pageSize)
    {
        var command = new GetPageTodoItemsQuery(new PageContext<ToDoItemFilter>(pageIndex, pageSize));
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.TotalCount.Should().Be(pageSize);
    }

    [Theory]
    [InlineData(1, 10, 1)]
    public async Task ShouldReturnItemPageById(int pageIndex, int pageSize, int id)
    {
        var command = new GetPageTodoItemsQuery(new PageContext<ToDoItemFilter>
            (pageIndex, pageSize, ToDoItemFilter.CreateBuilder().Id(id).Build()));

        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Data.FirstOrDefault().Should().NotBeNull();
        result.Value.Data.Count().Should().Be(1);
        result.Value.Data.First().Id.Should().Be(1);
    }

    [Theory]
    [InlineData(1, 10, "TestItem_1")]
    public async Task ShouldReturnItemPageByName(int pageIndex, int pageSize, string title)
    {
        var command = new GetPageTodoItemsQuery(new PageContext<ToDoItemFilter>(pageIndex, pageSize,
            ToDoItemFilter.CreateBuilder().Title(title).Build()));

        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Data.FirstOrDefault().Should().NotBeNull();
        result.Value.Data.Count().Should().Be(1);
        result.Value.Data.First().Title.Should().Be(TestItemTitle);
    }

    [Theory]
    [InlineData(1, 10, "TestItem_5")]
    public async Task ShouldReturnItemPageSorting(int pageIndex, int pageSize, string title)
    {
        var command = new GetPageTodoItemsQuery(new PageContext<ToDoItemFilter>(pageIndex, pageSize)
        {
            ListSort = new[] { new SortDescriptor("title", EnumSortDirection.Desc) }
        });

        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Data.FirstOrDefault().Should().NotBeNull();
        result.Value.Data.First().Title.Should().Be(title);
    }
}
