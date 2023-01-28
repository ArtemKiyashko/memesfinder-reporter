using System;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class WeeklyReportProvider : IReportProvider<IWeeklyReport>
    {
        private readonly IEnumerable<IWeeklyReport> _weeklyReports;

        public WeeklyReportProvider(IEnumerable<IWeeklyReport> weeklyReports)
		{
            _weeklyReports = weeklyReports;
        }

        public IEnumerable<IWeeklyReport> GetReports() => _weeklyReports;
    }
}

