using System;
using MemesFinderReporter.Interfaces.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace MemesFinderReporter.Managers.Reports.Extensions
{
	public static class ServiceCollectionExtensions
	{
        public static IServiceCollection AddWeeklyReports(this IServiceCollection services)
        {
            services.AddTransient<IReportProvider<IWeeklyReport>, WeeklyReportProvider>();
            services.AddTransient<IWeeklyReport, ContentMakersReport>();
            services.AddTransient<IWeeklyReport, MessagesByDaysReport>();
            services.AddTransient<IWeeklyReport, NewChatMembersReport>();
            return services;
        }

    }
}

