namespace NKZSoft.Template.Presentation.Rest.Controllers.Version20;

[ApiVersion(VersionController.VersionTwo)]
[Route("api/v{version:apiVersion}/to-do-items")]
public class ToDoItemControllerV2 : BaseController
{
    public ToDoItemControllerV2(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDtoBase<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDtoBase<Guid>>> CreateAsync(
        [FromBody] CreateToDoItemCommand command,
        CancellationToken cancellationToken)
        => (await Mediator.Send(command, cancellationToken).ConfigureAwait(false))
            .ToResultDto();
}
