namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class DeleteFailureException : Exception
{
    public DeleteFailureException(string name, object key, string message)
        : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
    {
    }

    private DeleteFailureException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
