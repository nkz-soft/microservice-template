using NKZSoft.Template.Application.Common.Paging;
using NKZSoft.Template.Application.Models;
using NKZSoft.Template.Application.TodoItems.Models;
using NKZSoft.Template.Application.TodoItems.Queries.Get;
using NKZSoft.Template.Application.TodoItems.Queries.GetPage;
using NKZSoft.Template.Common;
using NKZSoft.Template.Presentation.REST.Models;

namespace NKZSoft.Template.Presentation.REST.Controllers.Version10;

using Models;

[ApiVersion(VersionController.Version10)]
[Route("api/v{version:apiVersion}/to-do-items")]
public class ToDoItemController : BaseController
{
    public ToDoItemController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("page")]
    [ProducesResponseType(typeof(ResultDto<CollectionViewModel<ToDoItemDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<CollectionViewModel<ToDoItemDto>>>> Page(PageContext<ToDoItemFilter> pageContext)
        => (await Mediator.Send(GetPageTodoItemsQuery.Create(pageContext))).ToResultDto();

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResultDto<ToDoItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<Unit>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResultDto<ToDoItemDto>>> Get(int id)
        => (await Mediator.Send(new GetTodoItemQuery(id))).ToResultDto();

}
