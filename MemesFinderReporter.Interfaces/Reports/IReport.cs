using System;
using Azure.Monitor.Query.Models;

namespace MemesFinderReporter.Interfaces.Reports
{
	public interface IReport
	{
        public string GetReportQuery(long chatId);
        public string GetReportText();
        public Uri GetReportPictureUri(LogsQueryResult logsQueryResult);
    }
}

