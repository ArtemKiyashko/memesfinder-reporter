using System;
using MemesFinderReporter.Models.Reports;

namespace MemesFinderReporter.Interfaces.Reports
{
	public interface IReportManager<T> where T : IReport
    {
		public Task<IEnumerable<Report>> GetReportsResults();
	}
}

