namespace NKZSoft.Template.Application.Common.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException(string message)
        : base(message)
    {
    }
}