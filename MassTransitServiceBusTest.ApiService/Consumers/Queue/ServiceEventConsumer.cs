using MassTransit;
using MassTransitServiceBusTest.Contracts;

namespace MassTransitServiceBusTest.ApiService.Consumers.Queue;

public class ServiceEventConsumer(ILogger<ServiceEventConsumer> logger)
    : IConsumer<ServiceEvent>
{
    public Task Consume(ConsumeContext<ServiceEvent> context)
    {
        logger.LogInformation("Received service event with message: {Message}", context.Message);
        return Task.CompletedTask;
    }
}