using Microsoft.AspNetCore.Http;

namespace NKZSoft.Template.Common.Http;

public static class HttpHelper
{
    private const string MediaType = "application/json";

    public static StringContent GetRequestContent(object obj)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return new StringContent(JsonSerializer.Serialize(obj, options), Encoding.UTF8, MediaType);
    }

    public static async Task<T?> GetContentAsync<T>(this HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<T>(stringResponse, options);
    }

    public static async Task UpdateResponse<T>(this HttpResponse response, T data, int code)
    {
        var result = JsonSerializer.Serialize(data);

        response.ContentType = MediaType;
        response.StatusCode = code;
        await response.WriteAsync(result);
    }
}
