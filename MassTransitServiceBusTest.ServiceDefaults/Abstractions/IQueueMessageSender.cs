namespace MassTransitServiceBusTest.ServiceDefaults.Abstractions;

public interface IQueueMessageSender
{
    ValueTask SendAsync<TMessage>(
        string queueReference,
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class;
}