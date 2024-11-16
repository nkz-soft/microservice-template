namespace NKZSoft.Template.Presentation.Rest.Models.Result;

public static class ResultDtoExtensions
{
    public static ResultDtoBase<T> ToResultDto<T>(this Result<T> result) =>
        new(result.Value, result.IsSuccess, TransformErrors(result.Errors));

    private static IEnumerable<ErrorDto> TransformErrors(IEnumerable<IError> errors) =>
        errors.Select(TransformError);

    private static ErrorDto TransformError(IError error) => new(error.Message, TransformErrorCode(error));

    private static string? TransformErrorCode(IReason error)
    {
        return error.Metadata?.TryGetValue("ErrorCode", out var errorCode) != null &&
               errorCode != null ?
                   errorCode as string :
                   null;
    }
}
