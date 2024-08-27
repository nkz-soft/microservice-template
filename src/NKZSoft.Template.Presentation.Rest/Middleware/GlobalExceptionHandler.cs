namespace NKZSoft.Template.Presentation.Rest.Middleware;

using Common.Extensions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.ApplicationUnhandledException(exception);

        var problemDetails = new ProblemDetails { Instance = httpContext.Request.Path };
        if (exception is ValidationException fluentException)
        {
            problemDetails.Title = "one or more validation errors occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var validationErrors =
                fluentException.Errors.Select(error => error.ErrorMessage).ToList();
            problemDetails.Extensions.Add("errors", validationErrors);
        }
        else
        {
            problemDetails.Title = exception.Message;
        }

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
