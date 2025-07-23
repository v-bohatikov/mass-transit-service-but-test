using Aspire.Hosting.Azure;
using Azure.Provisioning.ServiceBus;
using SharedKernel;

namespace MassTransitServiceBusTest.AppHost;

public static class AzureInfraExtensions
{
    public static IResourceBuilder<AzureServiceBusResource> AddAzureServiceBusQueues(
        this IDistributedApplicationBuilder builder)
    {
        // Configuration for production environment which will be hosted on Azure.
        var serviceBus = builder
            .AddAzureServiceBus(ApplicationReferences.ServiceBusResourceName)
            .ConfigureInfrastructure(infra =>
            {
                var serviceBusNamespace = infra.GetProvisionableResources()
                    .OfType<ServiceBusNamespace>()
                    .Single();

                // We are using 'Basic' provisioning for service bus because of limitations of
                // free tier Azure account.
                serviceBusNamespace.Sku = new ServiceBusSku
                {
                    Name = ServiceBusSkuName.Basic
                };
            });

        // Configuration for other environments should support local execution via emulators of
        // Azure resources.
        if (!builder.ExecutionContext.IsPublishMode)
        {
            serviceBus = serviceBus.RunAsEmulator(cfg =>
                cfg.WithLifetime(ContainerLifetime.Persistent));
        }

        serviceBus.AddServiceBusQueue(ApplicationReferences.ServiceQueueResourceName);

        return serviceBus;
    }
}