using System;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class MessagesByDaysReport : IWeeklyReport
	{
		public MessagesByDaysReport()
		{
		}

        public Uri GetReportPictureUri()
        {
            throw new NotImplementedException();
        }

        public string GetReportText()
        {
            throw new NotImplementedException();
        }
    }
}

