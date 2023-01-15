namespace NKZSoft.Template.Presentation.Grpc.Models.ToDoItem;

using Result;

[ProtoContract]
public sealed record ToDoItemsResponse : ResultResponse
{
    [ProtoMember(1)]
    public ToDoItem[] Items { get; init; } = Array.Empty<ToDoItem>();
}
