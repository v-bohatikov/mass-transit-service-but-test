using MassTransit;
using MassTransitServiceBusTest.ServiceDefaults.Abstractions;

namespace MassTransitServiceBusTest.ServiceDefaults;

public class QueueMessageSender(IBus bus)
    : IQueueMessageSender
{
    public async ValueTask SendAsync<TMessage>(
        string queueReference,
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class
    {
        var sendEndpoint = await bus.GetSendEndpoint(new Uri($"queue:{queueReference}"));
        await sendEndpoint.Send(message, cancellationToken);
    }
}