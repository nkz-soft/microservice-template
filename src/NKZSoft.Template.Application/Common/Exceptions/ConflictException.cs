namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class ConflictException : Exception
{
    public ConflictException(string name, object key)
        : base($"Entity {name} with key {key} is already exist")
    {
    }

    public ConflictException(string message)
        : base(message)
    {
    }

    private ConflictException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
