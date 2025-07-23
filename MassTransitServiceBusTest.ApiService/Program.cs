using MassTransitServiceBusTest.ApiService.Consumers.Queue;
using MassTransitServiceBusTest.Contracts;
using MassTransitServiceBusTest.ServiceDefaults;
using MassTransitServiceBusTest.ServiceDefaults.Abstractions;
using SharedKernel;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add application services.
builder.ConfigureServiceBusWithQueueConsumersFromNamespaceContaining<QueueConsumersIndicator>(
    ApplicationReferences.ServiceQueueResourceName);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/test", async (IQueueMessageSender sender, CancellationToken cancellationToken) =>
    {
        try
        {
            var serviceEvent = new ServiceEvent("Hello world");
            await sender.SendAsync(
                ApplicationReferences.ServiceQueueResourceName,
                serviceEvent,
                cancellationToken);

            Results.Accepted();
        }
        catch (Exception e)
        {
            Results.InternalServerError(e);
        }
    })
    .WithOpenApi()
    .WithName("Test send queue message");

app.MapDefaultEndpoints();

app.Run();
