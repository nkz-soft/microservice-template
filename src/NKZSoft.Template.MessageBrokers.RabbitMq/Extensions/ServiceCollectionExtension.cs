namespace NKZSoft.Template.MessageBrokers.RabbitMq.Extensions;

using Consumers;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds MassTransit and its dependencies to the <see cref="IServiceCollection"/>, and allows consumers, sagas, and activities to be configured
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the MassTransits to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing settings to be used.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configuration, AddConsumers, AddReceiveEndpoints);
        return services;
    }

    private static void AddConsumers(IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<ToDoItemCreatedIntegrationEventConsumer>();
        configurator.AddConsumer<ToDoItemUpdatedIntegrationEventConsumer>();
    }

    private static void AddReceiveEndpoints(IRabbitMqBusFactoryConfigurator factoryConfigurator, IRegistrationContext registrationContext) =>
        factoryConfigurator.ReceiveEndpoint(configure =>
        {
            configure.ConfigureConsumer<ToDoItemCreatedIntegrationEventConsumer>(registrationContext);
            configure.ConfigureConsumer<ToDoItemUpdatedIntegrationEventConsumer>(registrationContext);
        });
}
