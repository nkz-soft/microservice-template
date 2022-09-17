using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Persistence.PostgreSQL.Database.Configurations;

public class ThingConfiguration : AuditableConfiguration<ToDoItem>, IEntityTypeConfiguration<ToDoItem>
{
    public override void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        base.Configure(builder);
            
        builder.ToTable("ToDoItems");

        builder.HasKey(e => e.Id);
            
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(256);
            
        builder.Property(e => e.Note)
            .HasMaxLength(512);
    }
}