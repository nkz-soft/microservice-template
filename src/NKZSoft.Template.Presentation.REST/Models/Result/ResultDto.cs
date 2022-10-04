namespace NKZSoft.Template.Presentation.REST.Models.Result;

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

public sealed record ResultDto<T>(T Data, bool IsSuccess, IEnumerable<ErrorDto> Errors) :
    ResultDtoBase(IsSuccess, Errors);
