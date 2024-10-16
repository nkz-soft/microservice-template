namespace NKZSoft.Template.Domain.Common;

public abstract class BaseAuditableEntity<TPKey, TUserPKey>(TPKey id)
    : BaseEntity<TPKey>(id), IBaseAuditableEntity<TUserPKey>
{
    public DateTime? Created { get; set; }

    public TUserPKey? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    public TUserPKey? ModifiedBy { get; set; }
}
