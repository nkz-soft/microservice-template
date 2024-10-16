namespace NKZSoft.Template.Persistence.PostgreSQL.Database.Configurations;

public class ToDoItemConfiguration : AuditableConfiguration<ToDoItem>, IEntityTypeConfiguration<ToDoItem>
{
    public override void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("ToDoItems");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(entity => entity.Note)
            .HasMaxLength(512);
    }
}
