namespace NKZSoft.Template.Common.Tests;

public class JsonContent<T> : StringContent
    where T : class
{
    private static JsonSerializerSettings Settings { get; } = new() { TypeNameHandling = TypeNameHandling.All };

    public JsonContent(T obj)
        : base(JsonConvert.SerializeObject(obj, Settings), Encoding.UTF8, "application/json")
    {
    }
}
