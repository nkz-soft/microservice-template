namespace NKZSoft.Template.Presentation.Rest.Controllers.Version10;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/to-do-items")]
public class ToDoItemControllerV1 : BaseController
{
    public ToDoItemControllerV1(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<ToDoItemDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<ToDoItemDto>>>> Page(
        [FromBody]PageContext<ToDoItemFilter> pageContext,
        CancellationToken cancellationToken)
        => (await Mediator.Send(GetPageTodoItemsQuery.Create(pageContext), cancellationToken)).ToResultDto();

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<ToDoItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ToDoItemDto>>> Get(Guid id, CancellationToken cancellationToken)
        => (await Mediator.Send(new GetTodoItemQuery(id), cancellationToken)).ToResultDto();

}
