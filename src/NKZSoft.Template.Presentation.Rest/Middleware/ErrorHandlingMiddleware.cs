namespace NKZSoft.Template.Presentation.Rest.Middleware;

using Common.Extensions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext, CancellationToken cancellationToken = default)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, _logger, ex, cancellationToken).ConfigureAwait(false);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context,
        ILogger log,
        Exception exception,
        CancellationToken cancellationToken)
    {
        log.ApplicationUnhandledException(exception);

        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var resultDto = new ResultDtoBase<Unit>(Unit.Value, IsSuccess:false,
            Errors:new[] { new ErrorDto(exception.Message, code.ToString()) });

        var result = JsonSerializer.Serialize(resultDto);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(result, cancellationToken).ConfigureAwait(false);
    }
}
