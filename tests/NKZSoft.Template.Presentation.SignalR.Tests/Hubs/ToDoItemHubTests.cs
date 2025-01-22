[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace NKZSoft.Template.Presentation.SignalR.Tests.Hubs;

using Common;
using Template.Common.Tests.Ordering;

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

        await Task.Delay(500, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Test");

        await connection.StopAsync(TestContext.Current.CancellationToken);
    }
}
