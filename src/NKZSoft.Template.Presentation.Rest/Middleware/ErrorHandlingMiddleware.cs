namespace NKZSoft.Template.Presentation.Rest.Middleware;

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

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, _logger, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, ILogger log, Exception exception)
    {
#pragma warning disable CA1848
        log.LogError(exception, "Application: An unhandled exception has occurred");
#pragma warning restore CA1848

        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var resultDto = new ResultDto<Unit>(Unit.Value, false, new[] { new ErrorDto(exception.Message, code.ToString()) });

        var result = JsonSerializer.Serialize(resultDto);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(result);
    }
}
