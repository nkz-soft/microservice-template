[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace NKZSoft.Template.Presentation.Grpc.Tests.Services;

using Common;
using global::Grpc.Core;
using Models;
using Template.Common.Tests.Ordering;

[Collection(nameof(GrpcCollectionDefinition))]
public sealed class ToDoItemServiceTests
{
    private readonly GrpcWebApplicationFactory<Program> _factory;
    private readonly IToDoItemService _service;

    public ToDoItemServiceTests(GrpcWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _service = _factory.CreateGrpcService<IToDoItemService>();
    }

    [Fact, Order(1)]
    public async Task GetPageItemsTestAsync()
    {
        var count = 0;

        await foreach (var response in _service.GetRageToDoItemsAsync(new GetPageTodoItemsRequest { PageIndex = 1, PageSize = 2 },
                           TestContext.Current.CancellationToken))
        {
            response.Items.Should().HaveCount(2);
            ++count;
        }

        count.Should().Be(2);
    }

    [Fact, Order(2)]
    public async Task GetPageItemByIdTestAsync()
    {
        await foreach (var item in _service.GetRageToDoItemsAsync
                           (new GetPageTodoItemsRequest { PageIndex = 1, PageSize = 1 },
                               TestContext.Current.CancellationToken))
        {
            var response = await _service.GetToDoItemByIdAsync(new GetTodoItemRequest
            {
                Id = item.Items[0].Id,
            },
            TestContext.Current.CancellationToken);

            response.Should().NotBeNull();

            response.Item.Should().NotBeNull();
            response.Item!.Id.Should().Be(item.Items[0].Id);
        }
    }

    [Fact, Order(3)]
    public async Task GetItemByNoIdTestAsync()
    {
        await Assert.ThrowsAsync<RpcException>(ActAsync);

        async Task ActAsync()
        {
            await _service.GetToDoItemByIdAsync(new GetTodoItemRequest
            {
                Id = Guid.NewGuid(),
            }).ConfigureAwait(false);
        }
    }

    [Fact, Order(4)]
    public async Task GetItemsTestAsync()
    {
        var count = 0;

        await foreach (var response in _service.GetToDoItemsAsync(TestContext.Current.CancellationToken).ConfigureAwait(false))
        {
            response.Should().NotBeNull();
            ++count;
        }

        count.Should().Be(4);
    }
}
