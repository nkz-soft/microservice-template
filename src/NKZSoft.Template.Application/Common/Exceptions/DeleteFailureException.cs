namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class DeleteFailureException(string name, object key, string message)
    : Exception($"Deletion of entity \"{name}\" ({key}) failed. {message}");
