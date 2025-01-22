using NKZSoft.Template.Common.Tests.Ordering;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(typeof(TestCaseOrderer))]

namespace NKZSoft.Template.Presentation.Rest.Tests.Controllers;

using Common;
using Grpc.Models.ToDoItem;
using NKZSoft.Template.Presentation.Rest.Models.Result;
using RestSharp;
using Template.Common.Tests.Ordering;

[Collection(nameof(RestCollectionDefinition))]
public sealed class ToDoItemControllerTests(RestWebApplicationFactory<Program> factory)
{
    private const string ApiUrlBaseV1 = "api/v1/to-do-items";
    private const string ApiUrlBaseV2 = "api/v2/to-do-items";
    private const string ApiUrlBaseV3 = "api/v3/to-do-items";

    private static class Get
    {
        public static string GetToDoItem(Guid id) => $"{ApiUrlBaseV1}/{id}";
        public static string GetRedisToDoItem(Guid id) => $"{ApiUrlBaseV3}/{id}";
    }

    private static class Post
    {
        public static string GetPageToDoItem() => $"{ApiUrlBaseV1}/page";
        public static string CreateToDoItem() => ApiUrlBaseV2;
        public static string CreateRedisToDoItem() => ApiUrlBaseV3;
    }

    [Fact, Order(1)]
    public async Task<ResultDtoBase<CollectionViewModel<ToDoItemDto>>> GetPageTestAsync()
    {
        var client = new RestClient(factory.CreateClient());

        var command = new PageContext<ToDoItemFilter>(1, 10);

        var response = await client.PostAsync<ResultDtoBase<CollectionViewModel<ToDoItemDto>>>(
            new RestRequest (Post.GetPageToDoItem()).AddJsonBody(command),
            TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDtoBase<CollectionViewModel<ToDoItemDto>>>();
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

        var client = new RestClient(factory.CreateClient());

        var response = await client.GetAsync<ResultDtoBase<ToDoItemDto>>(
            new RestRequest(Get.GetToDoItem(firstItem.Id)),
            TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDtoBase<ToDoItemDto>>();
        response!.IsSuccess.Should().BeTrue();

        response.Data.Should().NotBeNull();
        response.Data.Title.Should().Be(firstItem.Title);
        response.Data.Note.Should().Be(firstItem.Note);
    }

    [Fact, Order(3)]
    public async Task GetByIdNotFoundTestAsync()
    {
        var client = new RestClient(factory.CreateClient());

        var response = await client.GetAsync<ResultDtoBase<Unit>>(
            new RestRequest(Get.GetToDoItem(Guid.NewGuid())), TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDtoBase<Unit>>();
        response!.IsSuccess.Should().BeFalse();
    }

    [Fact, Order(4)]
    public async Task CreateTestAsync()
    {
        var client = new RestClient(factory.CreateClient());

        var command = new CreateToDoItemCommand("TestNote", ListId: null);

        var response = await client.PostAsync<ResultDtoBase<Guid>>(
            new RestRequest(Post.CreateToDoItem()).AddJsonBody(command),
            TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDtoBase<Guid>>();
        response!.IsSuccess.Should().BeTrue();
    }

    [Fact, Order(5)]
    public async Task<Guid> CreateRedisTestAsync()
    {
        var client = new RestClient(factory.CreateClient());

        var command = new CreateToDoItemCommand("TestRedisNote", ListId: null);

        var response = await client.PostAsync<ResultDtoBase<Guid>>(
            new RestRequest(Post.CreateRedisToDoItem()).AddJsonBody(command),
            TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDtoBase<Guid>>();
        response!.IsSuccess.Should().BeTrue();
        return await Task.FromResult(response.Data);
    }

    [Fact, Order(6)]
    public async Task GetByIdRedisTestAsync()
    {
        var id = await CreateRedisTestAsync();

        var client = new RestClient(factory.CreateClient());

        var response = await client.GetAsync<ResultDtoBase<ToDoItemDto>>(
            new RestRequest(Get.GetRedisToDoItem(id)),
            TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDtoBase<ToDoItemDto>>();
        response!.IsSuccess.Should().BeTrue();

        response.Data.Should().NotBeNull();
        response.Data.Title.Should().Be("TestRedisNote");
    }
}
