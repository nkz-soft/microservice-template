using NKZSoft.Template.Common.Http;
using NKZSoft.Template.Presentation.REST.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NKZSoft.Template.Presentation.REST.Middleware;

using Models;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, logger, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, ILogger log, Exception exception)
    {
        log.LogError(exception, "Application: An unhandled exception has occurred");

        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var resultDto = new ResultDto<Unit>(Unit.Value, false, new[] { new ErrorDto(exception.Message, code.ToString()) });

        await context.Response.UpdateResponse(resultDto, (int)code);
    }
}
