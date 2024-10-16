namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
    {
    }

    public ForbiddenAccessException(string? message) : base(message)
    {
    }

    public ForbiddenAccessException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
