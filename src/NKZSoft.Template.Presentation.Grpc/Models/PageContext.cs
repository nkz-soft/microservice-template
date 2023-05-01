namespace NKZSoft.Template.Presentation.Grpc.Models;

[ProtoContract]
[ProtoInclude(1, typeof(GetPageTodoItemsRequest))]
public record PageContext
{
    [ProtoMember(2)]
    public int PageIndex { get; init; }

    [ProtoMember(3)]
    public int PageSize { get; init; }
}
