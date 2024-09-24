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
    [ProducesResponseType(typeof(ResultDtoBase<ToDoItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDtoBase<ToDoItemDto>>> GetFromRedis(Guid id, CancellationToken cancellationToken)
        => (await Mediator.Send(new GetTodoItemQueryFromRedis(id), cancellationToken)).ToResultDto();

    [HttpPost]
    [ProducesResponseType(typeof(ResultDtoBase<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDtoBase<Guid>>> Create(
        [FromBody] CreateToDoItemRedisCommand command,
        CancellationToken cancellationToken)
        => (await Mediator.Send(command, cancellationToken)).ToResultDto();
}
