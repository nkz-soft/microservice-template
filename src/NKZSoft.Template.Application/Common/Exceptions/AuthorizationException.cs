namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class AuthorizationException : Exception
{
    public AuthorizationException(string message)
        : base(message)
    {
    }

    private AuthorizationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
