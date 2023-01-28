using System;
using Azure.Core;
using Azure.Identity;
using Azure.Monitor.Query;
using MemesFinderReporter.Managers.Reports.Extensions;
using MemesFinderReporter.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(MemesFinderReporter.Startup))]
namespace MemesFinderReporter
{
	public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<TelegramBotOptions>(_functionConfig.GetSection("TelegramBotOptions"));
            builder.Services.Configure<ReporterOptions>(_functionConfig.GetSection("ReporterOptions"));

            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.UseCredential(new DefaultAzureCredential());
                clientBuilder.AddClient<LogsQueryClient, LogsQueryClientOptions>((options, credentials) => new LogsQueryClient(credentials, options));
            });

            builder.Services.AddWeeklyReports();

            builder.Services.AddLogging();
        }
    }
}

