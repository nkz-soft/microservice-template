[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.SignalR.Tests.Hubs;

using Common;

[Collection(nameof(SignalRCollectionDefinition))]
public sealed class ToDoItemHubTest
{
    private readonly SignalRWebApplicationFactory<Program> _factory;

    public ToDoItemHubTest(SignalRWebApplicationFactory<Program> factory) => _factory = factory;

    [Fact, Order(1)]
    public async Task ToDoItemHubTestAsync()
    {
        await using var connection = await _factory.CreateConnectionAsync(nameof(ToDoItemHub));

        ToDoItemDto? result = null;
        connection.On<ToDoItemDto>(nameof(ToDoItemHub.ToDoItemUpdated), msg =>
        {
            result = msg;
        });

        await connection.InvokeAsync(nameof(ToDoItemHub.ToDoItemUpdated),
            new ToDoItemDto(Guid.NewGuid(), "Test", null, string.Empty, DateTime.Now,
                string.Empty, null, null), CancellationToken.None);

        await Task.Delay(500);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Test");

        await connection.StopAsync();
    }
}
