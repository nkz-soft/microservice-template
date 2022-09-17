namespace NKZSoft.Template.Application.Common.Filters;

public static class FilterDefinitionExtension
{
    public static bool HasValue<T>(this FilterFieldDefinition<T>? testValue)
    {
        return testValue switch
        {
            null => false,
            FilterFieldDefinition<string> stringValue => !string.IsNullOrEmpty(stringValue.Value),
            var nullableValue => nullableValue.Value != null
        };
    }
}