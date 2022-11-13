namespace NKZSoft.Template.Presentation.REST.Models.Result;

internal static class ResultDtoHelpers
{
    public static ResultDto<Unit> CreateFromErrors(string errors, HttpStatusCode statusCode)
    {
        var listErrors = new List<ErrorDto>(1)
        {
            new ErrorDto(errors, statusCode.ToString())
        };
        return new ResultDto<Unit>(Unit.Value, false, listErrors.ToArray());
    }
}
