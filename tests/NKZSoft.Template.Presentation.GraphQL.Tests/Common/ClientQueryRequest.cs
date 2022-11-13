namespace NKZSoft.Template.Presentation.GraphQL.Tests.Common;

internal sealed class ClientQueryRequest
{
    public const string GraphqlUrlBase = "/graphql";

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("operationName")]
    public string? OperationName { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("variables")]
    public Dictionary<string, object?>? Variables { get; set; }

    [JsonPropertyName("extensions")]
    public Dictionary<string, object?>? Extensions { get; set; }

    public override string ToString()
    {
        var query = new StringBuilder();

        if (Id is not null)
        {
            query.Append(CultureInfo.InvariantCulture,
                $"id={Id}");
        }

        if (Query is not null)
        {
            if (Id is not null)
            {
                query.Append('&');
            }
            query.Append(CultureInfo.InvariantCulture,
                $"query={Query.Replace("\r", "").Replace("\n", "")}");
        }

        if (OperationName is not null)
        {
            query.Append(CultureInfo.InvariantCulture,
                $"&operationName={OperationName}");
        }

        if (Variables is not null)
        {
            query.Append("&variables=" + JsonSerializer.Serialize(Variables));
        }

        if (Extensions is not null)
        {
            query.Append("&extensions=" + JsonSerializer.Serialize(Extensions));
        }

        return query.ToString();
    }
}
