namespace NKZSoft.Template.Presentation.GraphQL.Tests.Common;

public class ClientQueryRequest
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("operationName")]
    public string? OperationName { get; set; }

    [JsonProperty("query")]
    public string? Query { get; set; }

    [JsonProperty("variables")]
    public Dictionary<string, object?>? Variables { get; set; }

    [JsonProperty("extensions")]
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
            query.Append("&variables=" + JsonConvert.SerializeObject(Variables));
        }

        if (Extensions is not null)
        {
            query.Append("&extensions=" + JsonConvert.SerializeObject(Extensions));
        }

        return query.ToString();
    }
}
