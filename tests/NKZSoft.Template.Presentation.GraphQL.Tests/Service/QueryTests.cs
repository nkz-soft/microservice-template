[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.GraphQL.Tests.Service;

using Common;

[Collection(nameof(GraphQlCollection))]
public class ToDoItemControllerTest //: EfCoreCollection<GraphQLWebApplicationFactory<Program>>
{
    private const string GraphqlUrlBase = "/graphql";

    private readonly GraphQLWebApplicationFactory<Program> _factory;

    public ToDoItemControllerTest(GraphQLWebApplicationFactory<Program> factory) =>
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
