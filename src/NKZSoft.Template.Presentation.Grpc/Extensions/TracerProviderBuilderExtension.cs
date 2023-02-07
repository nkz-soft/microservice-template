namespace NKZSoft.Template.Presentation.Grpc.Extensions;

public static class TracerProviderBuilderExtension
{
    /// <summary>
    /// Enables the incoming requests automatic data collection for the grpc presentation level.
    /// </summary>
    /// <param name="builder"><see cref="TracerProviderBuilder"/> being configured.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The instance of <see cref="TracerProviderBuilder"/> to chain the calls.</returns>
    public static TracerProviderBuilder AddGrpcOpenTelemetry(
        this TracerProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.AddGrpcClientInstrumentation();
        return builder;
    }
}
