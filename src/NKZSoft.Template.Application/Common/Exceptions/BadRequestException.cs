namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class BadRequestException(string message) : Exception(message);
