namespace NKZSoft.Template.Presentation.GraphQL.Tests.Service;

using Common;

[Collection(nameof(GraphQlCollectionDefinition))]
public sealed class QueryTests(GraphQLWebApplicationFactory<Program> factory)
{
    [Fact, Order(1)]
    public async Task GetTodoItemsTestAsync()
    {
        var client = new RestClient(factory.CreateClient());

        const string query = "\r\n" +
                             "            query {\r\n" +
                             "                todoItems {\r\n" +
                             "                    id,\r\n" +
                             "                    note\r\n" +
                             "                }\r\n" +
                             "            }";

        var command = new ClientQueryRequest { Query = query };
        var response = await client.PostAsync(
            new RestRequest(ClientQueryRequest.GraphqlUrlBase)
                .AddJsonBody(command));

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
