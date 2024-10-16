namespace NKZSoft.Template.Application.Common.Filters;

public static class FilterDefinitionExtensions
{
    public static bool HasValue<T>([NotNullWhen(true)] this FilterFieldDefinition<T>? testValue) =>
        testValue switch
        {
            null => false,
            FilterFieldDefinition<string> stringValue => !string.IsNullOrEmpty(stringValue.Value),
            var nullableValue => nullableValue.Value is not null,
        };
}
