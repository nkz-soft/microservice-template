namespace NKZSoft.Template.Presentation.Rest.Controllers.Version20;

using Models.Result;

[ApiVersion(VersionController.Version20)]
[Route("api/v{version:apiVersion}/to-do-items")]
public class ToDoItemControllerV2 : BaseController
{
    public ToDoItemControllerV2(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
        [FromBody] CreateToDoItemCommand command,
        CancellationToken cancellationToken)
        => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
