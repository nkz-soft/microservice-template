﻿namespace NKZSoft.Template.Common.Tests;

using System.Text;
using Newtonsoft.Json;

public static class HttpHelper
{
    private const string MediaType = "application/json";
    public static StringContent GetRequestContent(object obj) =>
        new(JsonConvert.SerializeObject(obj), Encoding.UTF8, MediaType);

    public static async Task<T?> GetContentAsync<T>(this HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(stringResponse);
    }
}
