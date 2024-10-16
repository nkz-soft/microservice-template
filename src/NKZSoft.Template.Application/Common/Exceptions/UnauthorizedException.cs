namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class UnauthorizedException : Exception
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string? message) : base(message)
    {
    }

    public UnauthorizedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public static UnauthorizedException Response(string message) => new(message);
}
