[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.GRPC.Tests.Services;

using Xunit.Extensions.Ordering;

[Collection(nameof(GrpcCollectionDefinition))]
public sealed class ToDoItemServiceTest
{
    private readonly GrpcWebApplicationFactory<Program> _factory;

    public ToDoItemServiceTest(GrpcWebApplicationFactory<Program> factory) => _factory = factory;

    [Fact, Order(1)]
    public async Task GetItemsTestAsync()
    {
        var count = 0;
        var service = _factory.CreateGrpcService<IToDoItemService>();

        await foreach (var response in service.GetToDoItems(new GetTodoItemsRequest { PageIndex = 1, PageSize = 2 }))
        {
            response.Items.Should().HaveCount(2);
            ++count;
        }

        count.Should().Be(2);
    }

    [Fact, Order(2)]
    public async Task GetItemByIdTestAsync()
    {
        var service = _factory.CreateGrpcService<IToDoItemService>();

        await foreach(var item in service.GetToDoItems(new GetTodoItemsRequest { PageIndex = 1, PageSize = 1 }))
        {
            var response = await service.GetToDoItemById(new GetTodoItemRequest
            {
                Id = item.Items.First().Id
            });

            response.Should().NotBeNull();
            response.IsSuccess.Should().Be(true);

            response.Item.Should().NotBeNull();
            response.Item!.Id.Should().Be(item.Items.First().Id);
        }
    }


/*  We need proper error handling here
    [Fact, Order(3)]
    public async Task GetItemByNoIdTestAsync()
    {
        var service = _factory.CreateGrpcService<IToDoItemService>();

        var response = await service.GetToDoItemById(new GetTodoItemRequest
        {
            Id = Guid.NewGuid()
        });

        response.IsSuccess.Should().BeFalse();
        response.Errors.Length.Should().BeGreaterThan(0);
    }
*/
}
