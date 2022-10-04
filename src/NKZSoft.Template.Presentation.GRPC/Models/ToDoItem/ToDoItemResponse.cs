namespace NKZSoft.Template.Presentation.GRPC.Models.ToDoItem;

using Result;

[ProtoContract]
public sealed record ToDoItemResponse : ResultResponse
{
    [ProtoMember(1)]
    public ToDoItem? Item { get; init; }
}
