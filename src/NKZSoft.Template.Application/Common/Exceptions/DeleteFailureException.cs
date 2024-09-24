namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class DeleteFailureException : Exception
{
    public DeleteFailureException(string name, object key, string message)
        : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
    {
    }

    public DeleteFailureException()
    {
    }

    public DeleteFailureException(string? message) : base(message)
    {
    }

    public DeleteFailureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
