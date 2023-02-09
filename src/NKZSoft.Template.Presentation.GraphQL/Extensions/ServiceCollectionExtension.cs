namespace NKZSoft.Template.Presentation.GraphQL.Extensions;

using Common;
using Services;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds a GraphQL server configuration to the DI.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the GraphQL server to.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGraphQLPresentation(this IServiceCollection services)
    {
        //see https://github.com/ChilliCream/hotchocolate/issues/1975
        services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddType<QueryType<ToDoItem>>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddMutationType<Mutation>()
            .AddMutationConventions()
            .AddInstrumentation();

        return services;
    }
}
