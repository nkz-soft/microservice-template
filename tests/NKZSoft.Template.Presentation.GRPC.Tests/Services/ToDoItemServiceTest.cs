[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.Grpc.Tests.Services;

using Common;
using global::Grpc.Core;
using NKZSoft.Template.Presentation.Grpc.Models;
using NKZSoft.Template.Presentation.Grpc.Services;

[Collection(nameof(GrpcCollectionDefinition))]
public sealed class ToDoItemServiceTest
{
    private readonly GrpcWebApplicationFactory<Program> _factory;
    private readonly IToDoItemService _service;

    public ToDoItemServiceTest(GrpcWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _service = _factory.CreateGrpcService<IToDoItemService>();
    }

    [Fact, Order(1)]
    public async Task GetItemsTestAsync()
    {
        var count = 0;

        await foreach (var response in _service.GetToDoItems(new GetTodoItemsRequest { PageIndex = 1, PageSize = 2 }))
        {
            response.Items.Should().HaveCount(2);
            ++count;
        }

        count.Should().Be(2);
    }

    [Fact, Order(2)]
    public async Task GetItemByIdTestAsync()
    {
        await foreach(var item in _service.GetToDoItems(new GetTodoItemsRequest { PageIndex = 1, PageSize = 1 }))
        {
            var response = await _service.GetToDoItemById(new GetTodoItemRequest
            {
                Id = item.Items.First().Id
            });

            response.Should().NotBeNull();

            response.Item.Should().NotBeNull();
            response.Item!.Id.Should().Be(item.Items.First().Id);
        }
    }

    [Fact, Order(3)]
    public async Task GetItemByNoIdTestAsync()
    {
        async Task Act() =>  await _service.GetToDoItemById(new GetTodoItemRequest
        {
            Id = Guid.NewGuid()
        });

        await Assert.ThrowsAsync<RpcException>(Act);
    }
}
