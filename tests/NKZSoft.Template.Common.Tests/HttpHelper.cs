namespace NKZSoft.Template.Common.Tests;

public static class HttpHelper
{
    private const string MediaType = "application/json";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static StringContent GetRequestContent(object obj) =>
        new(JsonSerializer.Serialize(obj), Encoding.UTF8, MediaType);


    public static async Task<T?> GetContentAsync<T>(this HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(stringResponse, JsonSerializerOptions);
    }
}
