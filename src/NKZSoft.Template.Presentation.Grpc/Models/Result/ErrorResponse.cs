namespace NKZSoft.Template.Presentation.Grpc.Models.Result;

[ProtoContract]
public sealed record ErrorResponse
{
    public ErrorResponse(string message, string? code)
    {
        Message = message;
        Code = code;
    }

    [ProtoMember(1)]
    public string Message { get; init; }

    [ProtoMember(2)]
    public string? Code { get; init; }
}
