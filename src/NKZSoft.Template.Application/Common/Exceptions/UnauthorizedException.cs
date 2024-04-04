namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class UnauthorizedException(string message) : Exception(message)
{
    public static UnauthorizedException Response(string message)
        => new UnauthorizedException(message);
}
