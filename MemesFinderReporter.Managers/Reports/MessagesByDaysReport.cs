using System;
using Azure.Monitor.Query.Models;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class MessagesByDaysReport : IWeeklyReport
	{
		public MessagesByDaysReport()
		{
		}

        public Uri GetReportPictureUri(LogsQueryResult logsQueryResult)
        {
            throw new NotImplementedException();
        }

        public string GetReportQuery(long chatId)
        {
            throw new NotImplementedException();
        }

        public string GetReportText()
        {
            throw new NotImplementedException();
        }
    }
}

