using System;
namespace MemesFinderReporter.Interfaces.Reports
{
	public interface IReportProvider<T> where T : IReport
	{
		public IEnumerable<T> GetReports();
	}
}

