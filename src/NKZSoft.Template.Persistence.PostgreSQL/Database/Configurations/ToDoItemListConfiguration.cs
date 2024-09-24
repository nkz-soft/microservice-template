namespace NKZSoft.Template.Persistence.PostgreSQL.Database.Configurations;

public class ToDoItemListConfiguration : AuditableConfiguration<ToDoList>, IEntityTypeConfiguration<ToDoList>
{
    public override void Configure(EntityTypeBuilder<ToDoList> builder)
    {
        base.Configure(builder);

        builder.ToTable("ToDoLists");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(256);

        var navigation = builder.Metadata.FindNavigation(nameof(ToDoList.ToDoItems));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
