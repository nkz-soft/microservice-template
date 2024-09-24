namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class AuthorizationException : Exception
{
    public AuthorizationException()
    {
    }

    public AuthorizationException(string? message) : base(message)
    {
    }

    public AuthorizationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
