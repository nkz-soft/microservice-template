namespace NKZSoft.Template.Presentation.REST.Models;

using Result;

public static class ResultDtoExtensions
{
    public static ResultDto<T> ToResultDto<T>(this Result<T> result) =>
        new(result.Value, result.IsSuccess, TransformErrors(result.Errors));

    private static IEnumerable<ErrorDto> TransformErrors(IEnumerable<IError> errors) =>
        errors.Select(TransformError);

    private static ErrorDto TransformError(IError error) => new(error.Message, TransformErrorCode(error));

    private static string? TransformErrorCode(IReason error)
    {
        if (error.Metadata?.TryGetValue("ErrorCode", out var errorCode) != null && errorCode != null)
        {
            return errorCode as string;
        }
        return null;
    }
}
