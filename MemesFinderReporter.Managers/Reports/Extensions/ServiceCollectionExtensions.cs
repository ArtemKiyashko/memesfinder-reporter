using System;
using Azure.Identity;
using Azure.Monitor.Query;
using MemesFinderReporter.Interfaces.Reports;
using MemesFinderReporter.Models.Reports;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemesFinderReporter.Managers.Reports.Extensions
{
	public static class ServiceCollectionExtensions
	{
        public static IServiceCollection AddReports(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IReportManager<IWeeklyReport>, WeeklyReportManager>();
            services.AddTransient<IWeeklyReport, ContentMakersReport>();
            services.AddTransient<IWeeklyReport, MessagesByDaysReport>();
            //services.AddTransient<IWeeklyReport, NewChatMembersReport>();

            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.UseCredential(new DefaultAzureCredential());
                clientBuilder.AddClient<LogsQueryClient, LogsQueryClientOptions>((options, credentials) => new LogsQueryClient(credentials, options));
            });

            services.Configure<ReporterOptions>(configuration.GetSection("ReporterOptions"));

            return services;
        }



    }
}

