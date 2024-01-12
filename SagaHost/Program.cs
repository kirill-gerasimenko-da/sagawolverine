// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Oakton;
using Oakton.Resources;
using SagaHost;
using Wolverine;
using Wolverine.AzureServiceBus;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;

var builder = Host.CreateDefaultBuilder(args);

var serviceBusConnection = "YOUR_CONNECTION_STRING";

var databaseConnection = "YOUR_CONNECTION_STRING";

builder
    .UseWolverine(
        (ctx, opts) =>
        {
            opts.Services.AddDbContextWithWolverineIntegration<SagaDbContext>(c =>
            {
                c.UseNpgsql(databaseConnection);
            });

            opts.PersistMessagesWithPostgresql(databaseConnection);
            opts.UseEntityFrameworkCoreTransactions();
            opts.Policies.AutoApplyTransactions();

            opts.UseAzureServiceBus(serviceBusConnection).SystemQueuesAreEnabled(false);

            opts.ListenToAzureServiceBusQueue("kir-fifo")
                .UseDurableInbox()
                .RequireSessions()
                .Sequential();
        }
    )
    // .UseResourceSetupOnStartup()
    .ApplyOaktonExtensions();

var app = builder.Build();

// await app.StartAsync();
await app.RunOaktonCommands(args);