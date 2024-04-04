namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class PermissionDeniedException(string message) : Exception(message);
