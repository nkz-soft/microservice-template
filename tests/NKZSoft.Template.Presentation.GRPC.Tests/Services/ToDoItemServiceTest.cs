namespace NKZSoft.Template.Presentation.GRPC.Tests.Services;

public class ToDoItemServiceTest  : IClassFixture<GrpcWebApplicationFactory<Startup>>
{
    private readonly GrpcWebApplicationFactory<Startup> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public ToDoItemServiceTest(GrpcWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
    }

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

    [Fact, Order(1)]
    public async Task GetItemByIdTestAsync()
    {
        var service = _factory.CreateGrpcService<IToDoItemService>();

        await foreach(var item in service.GetToDoItems(new GetTodoItemsRequest { PageIndex = 1, PageSize = 1 }))
        {
            _testOutputHelper.WriteLine(item.Items.First().Id.ToString());

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
}
