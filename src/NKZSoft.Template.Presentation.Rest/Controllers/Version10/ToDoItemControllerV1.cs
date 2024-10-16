namespace NKZSoft.Template.Presentation.Rest.Controllers.Version10;

[ApiVersion(VersionController.VersionOne)]
[Route("api/v{version:apiVersion}/to-do-items")]
public class ToDoItemControllerV1 : BaseController
{
    public ToDoItemControllerV1(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDtoBase<CollectionViewModel<ToDoItemDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDtoBase<CollectionViewModel<ToDoItemDto>>>> PageAsync(
        [FromBody]PageContext<ToDoItemFilter> pageContext,
        CancellationToken cancellationToken)
        => (await Mediator.Send(GetPageTodoItemsQuery.Create(pageContext), cancellationToken)
                .ConfigureAwait(false))
            .ToResultDto();

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDtoBase<ToDoItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDtoBase<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDtoBase<ToDoItemDto>>> GetAsync(Guid id, CancellationToken cancellationToken)
        => (await Mediator.Send(new GetTodoItemQuery(id), cancellationToken).ConfigureAwait(false))
            .ToResultDto();
}
