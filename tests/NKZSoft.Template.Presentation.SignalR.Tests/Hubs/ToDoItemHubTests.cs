[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.SignalR.Tests.Hubs;

using Common;

[Collection(nameof(SignalRCollectionDefinition))]
public sealed class ToDoItemHubTests(SignalRWebApplicationFactory<Program> factory)
{
    [Fact, Order(1)]
    public async Task ToDoItemHubTestAsync()
    {
        await using var connection = await factory.CreateConnectionAsync(nameof(ToDoItemHub));

        ToDoItemDto? result = null;
        connection.On<ToDoItemDto>(nameof(ToDoItemHub.ToDoItemUpdated), msg => result = msg);

        await connection.InvokeAsync(nameof(ToDoItemHub.ToDoItemUpdated),
            new ToDoItemDto(Guid.NewGuid(), "Test", "Description",
                string.Empty, DateTime.Now, string.Empty, Modified: null, Deleted: null), CancellationToken.None);

        await Task.Delay(500);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Test");

        await connection.StopAsync();
    }
}
