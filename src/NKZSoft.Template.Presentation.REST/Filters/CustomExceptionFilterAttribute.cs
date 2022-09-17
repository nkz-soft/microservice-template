using NKZSoft.Template.Common;

namespace NKZSoft.Template.Presentation.REST.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CustomExceptionFilterAttribute(IWebHostEnvironment webHostEnvironment) =>
        _webHostEnvironment = webHostEnvironment.ThrowIfNull(nameof(webHostEnvironment));

    public override void OnException(ExceptionContext context)
    {
        Log.Error(context.Exception, "An unhandled exception has occurred");

        context.HttpContext.Response.ContentType = "application/json";

        var (actionResult, statusCode) = ResultFactory.CreatedResult(context, _webHostEnvironment.IsProduction());
        context.HttpContext.Response.StatusCode = statusCode;
        context.Result = actionResult;
    }
}
