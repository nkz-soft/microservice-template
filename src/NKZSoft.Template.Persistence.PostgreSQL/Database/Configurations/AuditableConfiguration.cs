namespace NKZSoft.Template.Persistence.PostgreSQL.Database.Configurations;

public class AuditableConfiguration<T> where T : class, IBaseAuditableEntity<string>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(entity => entity.Created)
            .IsRequired();

        builder.Property(entity => entity.CreatedBy)
            .IsRequired();

        builder.Property(entity => entity.Modified);

        builder.Property(entity => entity.ModifiedBy);
    }
}
