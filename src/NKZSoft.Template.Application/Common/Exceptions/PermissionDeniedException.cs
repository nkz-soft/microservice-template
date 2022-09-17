namespace NKZSoft.Template.Application.Common.Exceptions;

public class PermissionDeniedException : Exception
{
    public PermissionDeniedException(string message)
        : base(message)
    {
    }
}