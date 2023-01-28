using System;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class ContentMakersReport : IWeeklyReport
	{
		public ContentMakersReport()
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

