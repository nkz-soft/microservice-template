namespace NKZSoft.Template.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IBaseAuditableEntity
{
    public DateTime? Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    public string? ModifiedBy { get; set; }
}
