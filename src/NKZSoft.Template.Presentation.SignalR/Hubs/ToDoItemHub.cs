namespace NKZSoft.Template.Presentation.SignalR.Hubs;

public sealed class ToDoItemHub : Hub
{
    public async Task<Guid> ToDoItemUpdated(ToDoItemDto toDoItemDto)
    {
        await Clients.All.SendAsync(nameof(ToDoItemUpdated), toDoItemDto);
        return toDoItemDto.Id;
    }
}
