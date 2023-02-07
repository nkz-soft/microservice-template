namespace NKZSoft.Template.Presentation.Rest.Extensions;

public static class TracerProviderBuilderExtension
{
    /// <summary>
    /// Enables the incoming requests automatic data collection for the rest presentation level.
    /// </summary>
    /// <param name="builder"><see cref="TracerProviderBuilder"/> being configured.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The instance of <see cref="TracerProviderBuilder"/> to chain the calls.</returns>
    public static TracerProviderBuilder AddRestOpenTelemetry(
        this TracerProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        return builder.AddHttpClientInstrumentation();
    }
}
