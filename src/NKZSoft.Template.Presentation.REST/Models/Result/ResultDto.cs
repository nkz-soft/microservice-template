namespace NKZSoft.Template.Presentation.REST.Models.Result;

[JsonDerivedType(typeof(ResultDto<>), "ResultDto")]
public record ResultDtoBase(bool IsSuccess, IEnumerable<ErrorDto> Errors)
{
    public static ResultDto<Unit> CreateFromErrors(IList<KeyValuePair<string, string>> errors)
    {
        var listErrors = new List<ErrorDto>(errors.Count);

        foreach (var (key, value) in errors)
        {
            listErrors.Add(new ErrorDto(value, key));
        }
        return new ResultDto<Unit>(Unit.Value, false, listErrors.ToArray());
    }
    public static ResultDto<Unit> CreateFromErrors(string errors, HttpStatusCode statusCode)
    {
        var listErrors = new List<ErrorDto>(1)
        {
            new(errors, statusCode.ToString())
        };
        return new ResultDto<Unit>(Unit.Value, false, listErrors.ToArray());
    }
}

public sealed record ResultDto<T> : ResultDtoBase
{
    [JsonConstructor]
    public ResultDto(T Data, bool IsSuccess, IEnumerable<ErrorDto> Errors) : base(IsSuccess, Errors)
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
