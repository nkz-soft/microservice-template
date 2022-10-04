namespace NKZSoft.Template.Presentation.GRPC.Models;

[ProtoContract]
[ProtoInclude(1, typeof(GetTodoItemsRequest))]
public record PageContext
{
    [ProtoMember(2)]
    public int PageIndex { get; init; }

    [ProtoMember(3)]
    public int PageSize { get; init; }
}
