namespace NKZSoft.Template.Presentation.Rest.Controllers.Version30;

using Application.TodoItems.Commands.CreateInRedis;

[ApiVersion(VersionController.Version30)]
[Route("api/v{version:apiVersion}/to-do-items")]
public class ToDoItemControllerV3 : BaseController
{
    public ToDoItemControllerV3(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<ToDoItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ToDoItemDto>>> GetFromRedis(Guid id, CancellationToken cancellationToken)
        => (await Mediator.Send(new GetTodoItemQueryFromRedis(id), cancellationToken)).ToResultDto();

    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<Guid>>> Create(
        [FromBody] CreateToDoItemRedisCommand command,
        CancellationToken cancellationToken)
        => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
