using System;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class NewChatMembersReport : IWeeklyReport
	{
		public NewChatMembersReport()
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

