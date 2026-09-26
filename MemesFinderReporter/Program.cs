using Azure.Identity;
using Azure.Monitor.OpenTelemetry.Exporter;
using MemesFinderReporter.Managers.Reports.Extensions;
using MemesFinderReporter.Options;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;
using Telegram.Bot;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Azure.Messaging.ServiceBus.*"))
    .UseFunctionsWorkerDefaults()
    .UseAzureMonitorExporter();

builder.Services.Configure<TelegramBotOptions>(builder.Configuration.GetSection("TelegramBotOptions"));
builder.Services.AddSingleton<ITelegramBotClient>(provider =>
    new TelegramBotClient(provider.GetRequiredService<IOptions<TelegramBotOptions>>().Value.Token));
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.UseCredential(new DefaultAzureCredential());
    clientBuilder.AddClient<Azure.Monitor.Query.LogsQueryClient, Azure.Monitor.Query.LogsQueryClientOptions>(
    (options, credential) => new Azure.Monitor.Query.LogsQueryClient(credential, options));
});
builder.Services.AddReports(builder.Configuration);

builder.Build().Run();
