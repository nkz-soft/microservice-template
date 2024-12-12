namespace NKZSoft.Template.Common.Extensions;

public static class GuardClausesExtensions
{
    public static T ThrowIfNull<T>(this T param) where T : class? => Guard.Against.Null(param);

    public static T ThrowIfNull<T>(this T param, string paramName) where T : class? => Guard.Against.Null(param, paramName);

    public static T ThrowIfNull<T>(this T param, Exception exception) where T : class? => param ?? throw exception;
}
