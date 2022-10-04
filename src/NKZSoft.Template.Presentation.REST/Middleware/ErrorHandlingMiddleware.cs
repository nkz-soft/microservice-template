using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NKZSoft.Template.Presentation.REST.Middleware;

using Models;
using Models.Result;
using Newtonsoft.Json;

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
        log.LogError(exception, "Application: An unhandled exception has occurred");

        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var resultDto = new ResultDto<Unit>(Unit.Value, false, new[] { new ErrorDto(exception.Message, code.ToString()) });

        var result = JsonConvert.SerializeObject(resultDto);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(result);
    }
}
