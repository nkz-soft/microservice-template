namespace NKZSoft.Template.Presentation.Rest.Controllers;

using NKZSoft.Template.Common.Extensions;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    protected BaseController(IMediator mediator) => Mediator = mediator.ThrowIfNull();

    protected IMediator Mediator { get; }
}
