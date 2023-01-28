using System;
namespace MemesFinderReporter.Interfaces.Reports
{
	public interface IWeeklyReport : IReport
	{
		public Uri GetReportPictureUri();
		public string GetReportText();
	}
}

