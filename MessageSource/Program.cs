// See https://aka.ms/new-console-template for more information

using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.AzureServiceBus;

var builder = Host.CreateDefaultBuilder(args);

var serviceBusConnection = "YOUR_CONNECTION_STRING";

builder.UseWolverine(
    (ctx, opts) =>
    {
        opts.UseAzureServiceBus(serviceBusConnection).SystemQueuesAreEnabled(false);

        opts.PublishMessage<StartSagaMessage>()
            .ToAzureServiceBusQueue("kir-fifo")
            .UseDurableOutbox()
            .RequireSessions();

        opts.PublishMessage<UpdateEmailMessage>()
            .ToAzureServiceBusQueue("kir-fifo")
            .UseDurableOutbox()
            .RequireSessions();
    }
);

var sagaId = Guid.Parse("DC626099-2996-4C88-8796-9763974F6A19");

var app = builder.Build();

await app.StartAsync();

// 1. This is the first run - send message to start the saga
await app.Services.GetService<IMessageBus>()!
    .PublishAsync(
        new StartSagaMessage(sagaId, "Kirill"),
        new DeliveryOptions { GroupId = sagaId.ToString() }
    );

// 2. This is the second run - send message to update email on the saga
// await app.Services.GetService<IMessageBus>()!
//     .PublishAsync(
//         new UpdateEmailMessage(sagaId, "superman@email.com"),
//         new DeliveryOptions { GroupId = sagaId.ToString() }
//     );

Console.ReadLine();

await app.StopAsync();