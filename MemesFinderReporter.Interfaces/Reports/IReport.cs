using System;
using Azure.Monitor.Query.Models;

namespace MemesFinderReporter.Interfaces.Reports
{
	public interface IReport
	{
        public string GetReportQuery(long chatId);
        public string GetReportText(TimeSpan reportPeriod);
        public Uri GetReportPictureUri(LogsQueryResult logsQueryResult);
    }
}

