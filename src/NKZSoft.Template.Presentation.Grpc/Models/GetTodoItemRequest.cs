namespace NKZSoft.Template.Presentation.Grpc.Models;

[ProtoContract]
public sealed record GetTodoItemRequest
{
    [ProtoMember(1)]
    public Guid Id { get; init; }
}
