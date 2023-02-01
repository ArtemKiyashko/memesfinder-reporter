using Azure.Identity;
using Azure.Monitor.Query;
using MemesFinderReporter.Managers.Reports.Extensions;
using MemesFinderReporter.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

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
            builder.Services.AddSingleton<ITelegramBotClient>(factory => new TelegramBotClient(factory.GetService<IOptions<TelegramBotOptions>>().Value.Token));

            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.UseCredential(new DefaultAzureCredential());
                clientBuilder.AddClient<LogsQueryClient, LogsQueryClientOptions>((options, credentials) => new LogsQueryClient(credentials, options));
            });

            builder.Services.AddReports(_functionConfig);

            builder.Services.AddLogging();
        }
    }
}

