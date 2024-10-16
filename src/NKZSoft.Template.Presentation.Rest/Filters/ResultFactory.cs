namespace NKZSoft.Template.Presentation.Rest.Filters;

using NKZSoft.Template.Application.Common.Exceptions;
using Results;

public static class ResultFactory
{
    public static (IActionResult?, int) CreatedResult(ExceptionContext context, bool isProduction)
    {
        var errorMessage = isProduction ? context.Exception.Message : string
            .Join(';', context.Exception.Message, context.Exception.StackTrace);

        return context.Exception switch
        {
            ValidationException ex => (CreatedResult<BadRequestValidationObjectResult>(ex.Failures), (int)HttpStatusCode.BadRequest),
            NotFoundException => (CreatedResult<NotFoundObjectResult>(errorMessage, HttpStatusCode.NotFound), (int)HttpStatusCode.NotFound),
            UnauthorizedException => (CreatedResult<UnauthorizedObjectResult>(errorMessage, HttpStatusCode.Unauthorized), (int)HttpStatusCode.Unauthorized),
            UnprocessableEntityException => (CreatedResult<UnprocessableEntityObjectResult>(errorMessage, HttpStatusCode.UnprocessableEntity), (int)HttpStatusCode.UnprocessableEntity),
            ConflictException => (CreatedResult<ConflictObjectResult>(errorMessage, HttpStatusCode.Conflict), (int)HttpStatusCode.Conflict),
            PermissionDeniedException => (CreatedResult<ForbiddenObjectResult>(errorMessage, HttpStatusCode.Forbidden), (int)HttpStatusCode.Forbidden),
            BadRequestException => (CreatedResult<BadRequestObjectResult>(errorMessage, HttpStatusCode.BadRequest), (int)HttpStatusCode.BadRequest),
            _ => (new InternalServerErrorObjectResult(new ErrorDto(errorMessage,
                            nameof(HttpStatusCode.InternalServerError))), (int)HttpStatusCode.InternalServerError)
        };
    }

    private static IActionResult? CreatedResult<T>(IList<KeyValuePair<string, string>> errors)
        where T : ObjectResult =>
            (T?)Activator.CreateInstance(typeof(T), ResultDtoBase<Unit>.CreateFromErrors(errors));

    private static IActionResult? CreatedResult<T>(string error, HttpStatusCode statusCode)
        where T : ObjectResult =>
            (T?)Activator.CreateInstance(typeof(T), ResultDtoBase<Unit>.CreateFromErrors(error, statusCode));
}
