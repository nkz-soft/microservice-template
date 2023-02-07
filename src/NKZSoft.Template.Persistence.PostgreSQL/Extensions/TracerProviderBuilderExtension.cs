namespace NKZSoft.Template.Persistence.PostgreSQL.Extensions;

public static class TracerProviderBuilderExtension
{
    /// <summary>
    /// Enables the incoming requests automatic data collection for the PostgreSQL persistence level.
    /// </summary>
    /// <param name="builder"><see cref="TracerProviderBuilder"/> being configured.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The instance of <see cref="TracerProviderBuilder"/> to chain the calls.</returns>
    public static TracerProviderBuilder AddNgpSqlPersistenceOpenTelemetry(this TracerProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.AddEntityFrameworkCoreInstrumentation(o => o.SetDbStatementForText = true)
            .AddNpgsql();
        return builder;
    }
}
