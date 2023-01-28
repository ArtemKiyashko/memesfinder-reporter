using System;
using System.Threading.Tasks;
using MemesFinderReporter.Interfaces.Reports;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MemesFinderReporter
{
    public class WeeklyReporter
    {
        private readonly IReportManager<IWeeklyReport> _weeklyReportManager;

        public WeeklyReporter(IReportManager<IWeeklyReport> weeklyReportManager)
        {
            _weeklyReportManager = weeklyReportManager;
        }

        [FunctionName("weeklyreports")]
        public async Task RunWeeklyReports([TimerTrigger("0 0 19 * * 0",
        #if DEBUG
            RunOnStartup = true
        #endif
            )]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var r = await _weeklyReportManager.GetReportsResults();
        }
    }
}

