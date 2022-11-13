namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string name, object key)
        : base($"Entity {name} with key {key} is invalid")
    {
    }

    public UnprocessableEntityException(string message)
        : base(message)
    {
    }

    private UnprocessableEntityException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
