namespace NKZSoft.Template.Common.Tests.Ordering;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class OrderAttribute(int order) : Attribute
{
    public int Order { get; private set; } = order;
}
