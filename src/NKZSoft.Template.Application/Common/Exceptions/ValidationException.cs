namespace NKZSoft.Template.Application.Common.Exceptions;

[Serializable]
public sealed class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
    }

    public ValidationException(IReadOnlyCollection<ValidationFailure> failures)
        : this()
    {
        foreach (var failure in failures)
        {
            Failures.Add(new KeyValuePair<string, string>(failure.PropertyName, failure.ErrorMessage));
        }
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public IList<KeyValuePair<string, string>> Failures { get; } = new List<KeyValuePair<string, string>>();
}
