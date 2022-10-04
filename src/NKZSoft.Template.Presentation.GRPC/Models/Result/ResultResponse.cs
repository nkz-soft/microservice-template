namespace NKZSoft.Template.Presentation.GRPC.Models.Result;

using ToDoItem;

[ProtoInclude(1, typeof(ToDoItemResponse))]
[ProtoInclude(2, typeof(ToDoItemsResponse))]
[ProtoContract]
public record ResultResponse
{
    public ResultResponse(bool isSuccess, IEnumerable<ErrorResponse> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors.ToArray();
    }

    protected ResultResponse()
    {
    }

    [ProtoMember(3)]
    public bool IsSuccess { get; init; }


    [ProtoMember(4)]
    public ErrorResponse[] Errors { get; init; }
}

