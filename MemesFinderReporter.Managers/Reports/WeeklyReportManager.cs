using System;
using System.Collections.Concurrent;
using Azure.Monitor.Query;
using MemesFinderReporter.Interfaces.Reports;
using MemesFinderReporter.Models.Reports;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MemesFinderReporter.Managers.Reports
{
	public class WeeklyReportManager : IReportManager<IWeeklyReport>
	{
        private readonly IEnumerable<IWeeklyReport> _weeklyReports;
        private readonly ILogger<WeeklyReportManager> _logger;
        private readonly ReporterOptions _options;
        private readonly LogsQueryClient _logsQueryClient;

        public WeeklyReportManager(
            IEnumerable<IWeeklyReport> weeklyReports,
            IOptions<ReporterOptions> options,
            ILogger<WeeklyReportManager> logger,
            LogsQueryClient logsQueryClient)
		{
            _weeklyReports = weeklyReports;
            _logger = logger;
            _options = options.Value;
            _logsQueryClient = logsQueryClient;
        }

        public async Task<IEnumerable<Report>> GetReportsResults()
        {
            var result = new ConcurrentBag<Report>();

            foreach (var chat in _options.ChatOptions)
                await Parallel.ForEachAsync(_weeklyReports,
                    async (report, cancellationToken) => await ProcessReport(report, result, chat));

            return result;
        }

        private async Task ProcessReport(IWeeklyReport report, ConcurrentBag<Report> result, ChatOptions chat)
        {
            try
            {
                var logResult = await _logsQueryClient.QueryWorkspaceAsync(
                                workspaceId: _options.WorkspaceId,
                                query: report.GetReportQuery(chat.ChatId),
                                timeRange: new QueryTimeRange(TimeSpan.FromDays(7)));

                if (logResult.Value.Error is not null) throw new ArgumentException(logResult.Value.Error.Message);

                result.Add(new Report(
                    report.GetReportPictureUri(logResult.Value),
                    report.GetReportText(),
                    chat.ChatId,
                    chat.ReportsThreadId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating report for chatId: {chat.ChatId}, " +
                    $"worspaceId: {_options.WorkspaceId}; " +
                    $"Exception message: {ex.Message}");
            }
        }
    }
}

