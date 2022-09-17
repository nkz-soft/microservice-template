using NKZSoft.Template.Application.Models;
using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.TodoItems.Queries.Get;

using Application.Models;

public sealed record GetTodoItemQuery(int Id) : IRequest<Result<ToDoItemDto>>;