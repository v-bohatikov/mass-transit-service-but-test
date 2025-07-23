using MassTransitServiceBusTest.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var azureServiceBus = builder.AddAzureServiceBusQueues();

var apiService = builder
    .AddProject<Projects.MassTransitServiceBusTest_ApiService>("apiservice")
    .WithReference(azureServiceBus)
    .WaitFor(azureServiceBus);

builder.Build().Run();
