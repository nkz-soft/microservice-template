namespace NKZSoft.Template.Presentation.Rest.Controllers.Version20;

[ApiVersion(VersionController.Version20)]
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
    public async Task<ActionResult<ResultDtoBase<Guid>>> Create(
        [FromBody] CreateToDoItemCommand command,
        CancellationToken cancellationToken)
        => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
