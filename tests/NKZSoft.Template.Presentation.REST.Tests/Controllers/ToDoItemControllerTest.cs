namespace NKZSoft.Template.Presentation.REST.Tests.Controllers;

using Common;

public class ToDoItemControllerTest : IClassFixture<RestWebApplicationFactory<Startup>>
{
    private const string ApiUrlBase = "api/v1/to-do-items";

    private readonly RestWebApplicationFactory<Startup> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public ToDoItemControllerTest(RestWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
    }

    private static class Get
    {
        public static string GetToDoItem(Guid id) => $"{ApiUrlBase}/{id}";

    }

    private static class Post
    {
        public static string GetPageToDoItem() => $"{ApiUrlBase}/page";
    }

    [Fact, Order(1)]
    public async Task<ResultDto<CollectionViewModel<ToDoItemDto>>> GetPageTestAsync()
    {
        _testOutputHelper.WriteLine(_factory.Container.ConnectionString);

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
        _testOutputHelper.WriteLine(_factory.Container.ConnectionString);

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

    [Fact]
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
}
