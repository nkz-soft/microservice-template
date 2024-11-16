namespace NKZSoft.Template.Presentation.Rest.Models.Result;

[JsonDerivedType(typeof(ResultDtoBase<>), "ResultDtoBase")]
public record ResultDtoBase(bool IsSuccess, IEnumerable<ErrorDto> Errors)
{
    public static ResultDtoBase<Unit> CreateFromErrors(IList<KeyValuePair<string, string>> errors)
    {
        var listErrors = new List<ErrorDto>(errors.Count);

        foreach (var (key, value) in errors)
        {
            listErrors.Add(new ErrorDto(value, key));
        }
        return new ResultDtoBase<Unit>(Unit.Value, IsSuccess:false, [.. listErrors]);
    }
    public static ResultDtoBase<Unit> CreateFromErrors(string errors, HttpStatusCode statusCode)
    {
        var listErrors = new List<ErrorDto>(1)
        {
            new(errors, statusCode.ToString()),
        };
        return new ResultDtoBase<Unit>(Unit.Value, IsSuccess:false, [.. listErrors]);
    }
}

public sealed record ResultDtoBase<T> : ResultDtoBase
{
    [JsonConstructor]
    public ResultDtoBase(T Data, bool IsSuccess, IEnumerable<ErrorDto> Errors) : base(IsSuccess, Errors)
    {
        this.Data = Data;
    }

    public T Data { get; init; }

    public void Deconstruct(out T Data, out bool IsSuccess, out IEnumerable<ErrorDto> Errors)
    {
        Data = this.Data;
        IsSuccess = this.IsSuccess;
        Errors = this.Errors;
    }
}
