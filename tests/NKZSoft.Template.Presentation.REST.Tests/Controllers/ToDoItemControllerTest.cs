[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.REST.Tests.Controllers;

using Application.TodoItems.Commands.Create;
using Common;

[Collection(nameof(RestCollection))]
public class ToDoItemControllerTest //: EfCoreCollection<RestWebApplicationFactory<Program>>
{
    private const string ApiUrlBaseV1 = "api/v1/to-do-items";
    private const string ApiUrlBaseV2 = "api/v2/to-do-items";

    private readonly RestWebApplicationFactory<Program> _factory;

    public ToDoItemControllerTest(RestWebApplicationFactory<Program> factory) => _factory = factory;

    private static class Get
    {
        public static string GetToDoItem(Guid id) => $"{ApiUrlBaseV1}/{id}";

    }

    private static class Post
    {
        public static string GetPageToDoItem() => $"{ApiUrlBaseV1}/page";
        public static string CreateToDoItem() => $"{ApiUrlBaseV2}";
    }

    [Fact, Order(1)]
    public async Task<ResultDto<CollectionViewModel<ToDoItemDto>>> GetPageTestAsync()
    {
        var client = _factory.CreateClient();

        var command = new PageContext<ToDoItemFilter>(1, 10) ;

        var response = await client.PostAsync(Post.GetPageToDoItem(),
            new JsonContent<PageContext<ToDoItemFilter>>(command));

        response.EnsureSuccessStatusCode();
        var content = await response.GetContentAsync<ResultDto<CollectionViewModel<ToDoItemDto>>>();

        content.Should().NotBeNull();
        content.Should().BeOfType<ResultDto<CollectionViewModel<ToDoItemDto>>>();
        content!.IsSuccess.Should().BeTrue();

        content.Data.Should().NotBeNull();
        content.Data.Data.Should().HaveCount(5);

        return content;
    }

    [Fact, Order(2)]
    public async Task GetByIdTestAsync()
    {
        var items = await GetPageTestAsync();
        var firstItem = items.Data.Data.First();

        var client = _factory.CreateClient();

        var response = await client.GetAsync(Get.GetToDoItem(firstItem.Id));

        response.EnsureSuccessStatusCode();

        var content = await response.GetContentAsync<ResultDto<ToDoItemDto>>();

        content.Should().NotBeNull();
        content.Should().BeOfType<ResultDto<ToDoItemDto>>();
        content!.IsSuccess.Should().BeTrue();

        content.Data.Should().NotBeNull();
        content.Data.Title.Should().Be(firstItem.Title);
        content.Data.Note.Should().Be(firstItem.Note);
    }

    [Fact, Order(3)]
    public async Task GetByIdNotFoundTestAsync()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(Get.GetToDoItem(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var content = await response.GetContentAsync<ResultDto<Unit>>();

        content.Should().NotBeNull();
        content.Should().BeOfType<ResultDto<Unit>>();
        content!.IsSuccess.Should().BeFalse();
    }

    [Fact, Order(4)]
    public async Task CreateTestAsync()
    {
        var client = _factory.CreateClient();

        var command = new CreateToВoItemCommand("TestNote", null) ;

        var response = await client.PostAsync(Post.CreateToDoItem(),
            new JsonContent<CreateToВoItemCommand>(command));

        response.EnsureSuccessStatusCode();
        var content = await response.GetContentAsync<ResultDto<Guid>>();

        content.Should().NotBeNull();
        content.Should().BeOfType<ResultDto<Guid>>();
        content!.IsSuccess.Should().BeTrue();
    }
}
