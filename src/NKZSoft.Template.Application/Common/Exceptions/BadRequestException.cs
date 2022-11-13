namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    private BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
