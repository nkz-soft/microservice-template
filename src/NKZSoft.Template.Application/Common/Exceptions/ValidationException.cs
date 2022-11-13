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

    private ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }

    public IList<KeyValuePair<string, string>> Failures { get; } = new List<KeyValuePair<string, string>>();

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        base.GetObjectData(info, context);
        info.AddValue(nameof(Failures), this.Failures, typeof(IList<KeyValuePair<string, string>>));
    }
}
