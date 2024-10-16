namespace NKZSoft.Template.Application.Models;

using Common.Models;

public sealed record ToDoItemDto(Guid Id, string Title, string? Note, string CreatedBy, DateTime Created, string ModifiedBy, DateTime? Modified, DateTime? Deleted)
    : BaseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted);
