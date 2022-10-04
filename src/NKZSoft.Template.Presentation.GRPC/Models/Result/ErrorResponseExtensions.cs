namespace NKZSoft.Template.Presentation.GRPC.Models.Result;

public static class ErrorResponseExtensions
{
    public static ResultResponse ToResultDto<T>(this Result<T> result) =>
        new(result.IsSuccess, TransformErrors(result.Errors));

    private static IEnumerable<ErrorResponse> TransformErrors(IEnumerable<IError> errors) =>
        errors.Select(TransformError);

    private static ErrorResponse TransformError(IError error) => new(error.Message, TransformErrorCode(error));

    private static string? TransformErrorCode(IReason error)
    {
        if (error.Metadata?.TryGetValue("ErrorCode", out var errorCode) != null && errorCode != null)
        {
            return errorCode as string;
        }
        return null;
    }
}
