namespace NKZSoft.Template.Presentation.GRPC.Models;

[ProtoContract]
public sealed record GetTodoItemsRequest : PageContext
{
    [ProtoMember(1)]
    public Guid[] Ids { get; init; } = Array.Empty<Guid>();
}
