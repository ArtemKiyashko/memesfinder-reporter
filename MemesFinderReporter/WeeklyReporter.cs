using System;
using MemesFinderReporter.Interfaces.Reports;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MemesFinderReporter
{
    public class WeeklyReporter
    {
        private readonly IReportProvider<IWeeklyReport> _weeklyReportProvider;

        public WeeklyReporter(IReportProvider<IWeeklyReport> weeklyReportProvider)
        {
            _weeklyReportProvider = weeklyReportProvider;
        }

        [FunctionName("weeklyreports")]
        public void RunWeeklyReports([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}

