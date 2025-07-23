using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassTransitServiceBusTest.ServiceDefaults;

public class BusControlHostedService(IBusControl busControl) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await busControl.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await busControl.StopAsync(cancellationToken);
    }
}