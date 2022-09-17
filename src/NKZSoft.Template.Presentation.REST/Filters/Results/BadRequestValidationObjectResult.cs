using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NKZSoft.Template.Presentation.REST.Filters.Results;

public class BadRequestValidationObjectResult : BadRequestObjectResult
{
    public BadRequestValidationObjectResult([CanBeNull] object? error) : base(error)
    {
    }

    public BadRequestValidationObjectResult([NotNull] ModelStateDictionary modelState) : base(modelState)
    {
    }
}