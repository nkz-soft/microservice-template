namespace NKZSoft.Template.Presentation.Grpc.Models.ToDoItem;

using Result;

[ProtoContract]
public sealed record ToDoItemResponse : ResultResponse
{
    [ProtoMember(1)]
    public ToDoItemDto? Item { get; init; }
}
