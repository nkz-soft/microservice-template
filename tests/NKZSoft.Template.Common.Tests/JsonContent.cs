namespace NKZSoft.Template.Common.Tests;

public class JsonContent<T> : StringContent
    where T : class
{
    public JsonContent(T obj)
        : base(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json")
    {
    }
}
