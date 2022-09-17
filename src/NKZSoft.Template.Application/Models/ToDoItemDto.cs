using NKZSoft.Template.Application.Common.Models;

namespace NKZSoft.Template.Application.Models;

using Common.Models;

public sealed record ToDoItemDto(int Id,  string Title, string? Note, string CreatedBy, DateTime Created, string ModifiedBy, DateTime? Modified, DateTime? Deleted)
    : BaseDto(CreatedBy, Created, ModifiedBy, Modified, Deleted);