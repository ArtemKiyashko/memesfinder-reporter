using System;
using System.Threading.Tasks;
using MemesFinderReporter.Interfaces.Reports;
using Microsoft.Azure.WebJobs;
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
        public async Task RunWeeklyReports([TimerTrigger("%WeeklyReporterSchedule%",
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

