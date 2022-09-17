namespace NKZSoft.Template.Domain.Common;

public interface IBaseAuditableEntity : IEntity
{
    DateTime? Created { get; set; }

    string? CreatedBy { get; set; }

    DateTime? Modified { get; set; }

    string? ModifiedBy { get; set; }
}