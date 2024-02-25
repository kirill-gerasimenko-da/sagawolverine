// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Oakton;
using SagaHost;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;

var builder = Host.CreateDefaultBuilder(args);

var databaseConnection = "YOUR_CONNECTION_STRING";

builder
    .UseWolverine(
        (ctx, opts) =>
        {
            opts.Services.AddDbContextWithWolverineIntegration<SagaDbContext>(c =>
            {
                c.UseNpgsql(databaseConnection);
            });

            opts.CodeGeneration.ReferenceAssembly(typeof(Program).Assembly);

            opts.PersistMessagesWithPostgresql(databaseConnection);
            opts.UseEntityFrameworkCoreTransactions();
            opts.Policies.AutoApplyTransactions();
        }
    )
    // .UseResourceSetupOnStartup()
    .ApplyOaktonExtensions();

var app = builder.Build();

// await app.StartAsync();
await app.RunOaktonCommands(args);