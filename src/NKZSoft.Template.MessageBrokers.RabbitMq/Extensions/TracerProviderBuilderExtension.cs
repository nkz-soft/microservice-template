namespace NKZSoft.Template.MessageBrokers.RabbitMq.Extensions;

using OpenTelemetry.Trace;

public static class TracerProviderBuilderExtension
{
    /// <summary>
    /// Enables the incoming requests automatic data collection for the MassTransit.
    /// </summary>
    /// <param name="builder"><see cref="TracerProviderBuilder"/> being configured.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The instance of <see cref="TracerProviderBuilder"/> to chain the calls.</returns>
    public static TracerProviderBuilder AddMassTransitOpenTelemetry(this TracerProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        return builder.AddSource("MassTransit");
    }
}
