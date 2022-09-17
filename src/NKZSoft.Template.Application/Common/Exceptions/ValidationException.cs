namespace NKZSoft.Template.Application.Common.Exceptions;
using FluentValidation.Results;

public class ValidationException : Exception
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

    public IList<KeyValuePair<string, string>> Failures { get; } = new List<KeyValuePair<string, string>>();
}
