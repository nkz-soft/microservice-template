namespace NKZSoft.Template.Presentation.SignalR.Hubs;

public sealed class ToDoItemHub : Hub
{
    public async Task<Guid> ToDoItemUpdated(ToDoItemDto toDoItemDto)
    {
        //see https://github.com/dotnet/aspnetcore/issues/11542
        await Clients.All.SendAsync(nameof(ToDoItemUpdated), toDoItemDto, CancellationToken.None)
            .ConfigureAwait(false);
        return toDoItemDto.Id;
    }
}
