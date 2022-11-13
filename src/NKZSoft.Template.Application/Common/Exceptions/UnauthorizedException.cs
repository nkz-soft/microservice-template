namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public static UnauthorizedException Response(string message)
        => new UnauthorizedException(message);

    private UnauthorizedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
