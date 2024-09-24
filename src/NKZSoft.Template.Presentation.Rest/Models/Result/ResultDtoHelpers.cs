namespace NKZSoft.Template.Presentation.Rest.Models.Result;

internal static class ResultDtoHelpers
{
    public static ResultDtoBase<Unit> CreateFromErrors(string errors, HttpStatusCode statusCode)
    {
        var listErrors = new List<ErrorDto>(1)
        {
            new ErrorDto(errors, statusCode.ToString()),
        };
        return new ResultDtoBase<Unit>(Unit.Value, IsSuccess:false, listErrors.ToArray());
    }
}
