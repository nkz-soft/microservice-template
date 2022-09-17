using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NKZSoft.Template.Persistence.PostgreSQL.Extensions;

public static class ModelBuilderExtension
{
    public static void DataTimeConfigure(this ModelBuilder modelBuilder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }
}