[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.REST.Tests.Controllers;

using Common;
using Models.Result;
using RestSharp;

[Collection(nameof(RestCollectionDefinition))]
public sealed class ToDoItemControllerTest
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
        var client = new RestClient(_factory.CreateClient());

        var command = new PageContext<ToDoItemFilter>(1, 10);

        var response = await client.PostAsync<ResultDto<CollectionViewModel<ToDoItemDto>>>(
            new RestRequest (Post.GetPageToDoItem()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<CollectionViewModel<ToDoItemDto>>>();
        response!.IsSuccess.Should().BeTrue();

        response.Data.Should().NotBeNull();
        response.Data.Data.Should().HaveCount(5);

        return response;
    }

    [Fact, Order(2)]
    public async Task GetByIdTestAsync()
    {
        var items = await GetPageTestAsync();
        var firstItem = items.Data.Data.First();

        var client = new RestClient(_factory.CreateClient());

        var response = await client.GetAsync<ResultDto<ToDoItemDto>>(
            new RestRequest(Get.GetToDoItem(firstItem.Id)));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<ToDoItemDto>>();
        response!.IsSuccess.Should().BeTrue();

        response.Data.Should().NotBeNull();
        response.Data.Title.Should().Be(firstItem.Title);
        response.Data.Note.Should().Be(firstItem.Note);
    }

    [Fact, Order(3)]
    public async Task GetByIdNotFoundTestAsync()
    {
        var client = new RestClient(_factory.CreateClient());

        var response = await client.GetAsync<ResultDto<Unit>>(
            new RestRequest(Get.GetToDoItem(Guid.NewGuid())));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Unit>>();
        response!.IsSuccess.Should().BeFalse();
    }

    [Fact, Order(4)]
    public async Task CreateTestAsync()
    {
        var client = new RestClient(_factory.CreateClient());

        var command = new CreateToDoItemCommand("TestNote", null) ;

        var response = await client.PostAsync<ResultDto<Guid>>(
            new RestRequest(Post.CreateToDoItem()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Guid>>();
        response!.IsSuccess.Should().BeTrue();
    }
}
