using System;
using Azure.Monitor.Query.Models;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class NewChatMembersReport : IWeeklyReport
	{
		public NewChatMembersReport()
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

