using System;
using Azure;
using Azure.Monitor.Query.Models;
using ImageChartsLib;
using MemesFinderReporter.Interfaces.Reports;

namespace MemesFinderReporter.Managers.Reports
{
	public class ContentMakersReport : IWeeklyReport
	{
        public Uri GetReportPictureUri(LogsQueryResult logsQueryResult)
        {
            var calculationResult = CalculateTopResults(logsQueryResult);
            var chart = new ImageCharts()
                .cht("pd")
                .chs("800x600")
                .chdls("FFFFFF,14")
                .chf("bg,s,000000")
                .chlps("font.size,14")
                .chts("FFFFFF,30")
                .chtt("Top-10 Content Makers")
                .chco("ee4266,ffd23f,21fa90,57737a,84cae7")
                .chd($"a:{calculationResult.Select(cr => cr.Value.Value.ToString()).Aggregate((f, s) => $"{f},{s}")}")
                .chl($"{calculationResult.Select(cr => cr.Value.Percent.ToString("P2")).Aggregate((f, s) => $"{f}|{s}")}")
                .chdl($"{calculationResult.Select(cr => cr.Key).Aggregate((f, s) => $"{f}|{s}")}");

            return new Uri(chart.toURL());
        }

        public string GetReportQuery(long chatId) => @$"let chatId = ""{chatId}"";
            AppTraces
            | where ((OperationName == ""MemesFinderGateway""))
            | where Message startswith ""Update received: ""
            | extend tgUpdate = parse_json(replace_string(Message, ""Update received: "", """"))
            | where tostring(tgUpdate.message) != """" and tostring(tgUpdate.message.chat.id) == chatId
            | extend TgUser = tgUpdate.message.from.username
            | extend TgName = tgUpdate.message.from.first_name
            | extend TgSurname = tgUpdate.message.from.last_name
            | summarize Count = count() by strcat(tostring(TgUser), "" "", tostring(TgName), "" "", tostring(TgSurname))";

        public string GetReportText(TimeSpan reportPeriod)
            => $"Топ-10 контент-мейкеров\n\n#reports\n\n{DateTime.Now.Add(-reportPeriod).ToShortDateString()} - {DateTime.Now.ToShortDateString()}";

        private IDictionary<string, (decimal Percent, int Value)> CalculateTopResults(LogsQueryResult logsQueryResult)
        {
            var result = new Dictionary<string, (decimal Percent, int Value)>();
            var totalMessageCount = logsQueryResult.Table.Rows.Sum((row) => row.GetInt32(1));
            var outstandingValues = totalMessageCount;

            foreach (var row in logsQueryResult.Table.Rows.OrderByDescending(row => row.GetInt32(1)).Take(10))
            {
                result.Add(
                    row.GetString(0).Trim(),
                    ((decimal Percent, int Value))(Percent: (decimal)row.GetInt32(1) / totalMessageCount, Value: row.GetInt32(1).Value));

                outstandingValues -= row.GetInt32(1);
            }

            if (logsQueryResult.Table.Rows.Count > 10)
                result.Add("Others", ((decimal Percent, int Value))(Percent: (decimal)outstandingValues / totalMessageCount, Value: outstandingValues.Value));

            return result;
        }
    }
}

