[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace NKZSoft.Template.Presentation.GraphQL.Tests.Service;

using Common;

[Collection(nameof(GraphQlCollectionDefinition))]
public sealed class MutationTests
{
    private readonly GraphQLWebApplicationFactory<Program> _factory;

    public MutationTests(GraphQLWebApplicationFactory<Program> factory) =>
        _factory = factory;


    [Fact, Order(1)]
    public async Task CreateToDoItemsTestAsync()
    {
        var client = new RestClient(_factory.CreateClient());

        const string query = @"mutation {
              createToDoItem(input: {title: ""Test"", listId: null}) {
                    uUID
                }
            }";

        var command = new ClientQueryRequest { Query = query };

        var response = await client.PostAsync(
            new RestRequest(ClientQueryRequest.GraphqlUrlBase)
                .AddJsonBody(command));

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
