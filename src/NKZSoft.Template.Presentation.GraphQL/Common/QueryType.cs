namespace NKZSoft.Template.Presentation.GraphQL.Common;

internal sealed class QueryType<T> : ObjectType<T> where T : IEntity
{
    /// <summary>
    /// see https://github.com/ChilliCream/hotchocolate/issues/1975
    /// </summary>
    protected override void Configure(IObjectTypeDescriptor<T> descriptor) =>
        descriptor.Ignore(x => x.DomainEvents);
}
