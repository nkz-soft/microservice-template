namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }

    private ForbiddenAccessException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
