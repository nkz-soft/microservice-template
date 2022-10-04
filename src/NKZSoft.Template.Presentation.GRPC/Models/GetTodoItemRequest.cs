namespace NKZSoft.Template.Presentation.GRPC.Models;

[ProtoContract]
public sealed record GetTodoItemRequest
{
    [ProtoMember(1)]
    public Guid Id { get; init; }
}
