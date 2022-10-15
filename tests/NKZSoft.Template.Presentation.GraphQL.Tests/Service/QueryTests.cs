namespace NKZSoft.Template.Presentation.GraphQL.Tests.Service;

using Common;

public class ToDoItemControllerTest : IClassFixture<GraphQLWebApplicationFactory<Startup>>
{
    private const string GraphqlUrlBase = "/graphql";

    private readonly GraphQLWebApplicationFactory<Startup> _factory;

    public ToDoItemControllerTest(GraphQLWebApplicationFactory<Startup> factory) =>
        _factory = factory;

    [Fact, Order(1)]
    public async Task GetTodoItemsTestAsync()
    {
        var client = _factory.CreateClient();

        const string query = @"
            query {
                todoItems {
                    id,
                    note
                }
            }";

        var content = new JsonContent<ClientQueryRequest>(
            new ClientQueryRequest { Query = query });

        var response = await client.PostAsync(GraphqlUrlBase, content);

        response.EnsureSuccessStatusCode();
    }
}
