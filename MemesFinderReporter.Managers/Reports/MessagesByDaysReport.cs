﻿using System;
using System.Globalization;
using Azure.Monitor.Query.Models;
using ImageChartsLib;
using MemesFinderReporter.Interfaces.Reports;
using MemesFinderReporter.Managers.Reports.Extensions;

namespace MemesFinderReporter.Managers.Reports
{
	public class MessagesByDaysReport : IWeeklyReport
	{
        public Uri GetReportPictureUri(LogsQueryResult logsQueryResult)
        {
            
            var chart = new ImageCharts()
                .cht("bvs")
                .chs("800x600")
                .chdls("FFFFFF,14")
                .chf("bg,s,000000")
                .chlps("font.size,30")
                .chts("FFFFFF,30")
                .chtt("Messages by Days")
                .chbr("8")
                .chxt("x,y")
                .chma("0,0,10,10")
                .chg("0,20,0,0,0,0,646464")
                .chxs("0,FFFFFF,20|1,FFFFFF,20")
                .chdl("New messages|Edited messages")
                .chd($"a:{logsQueryResult.Table.Rows.Select(r => r.GetInt32(1).ToString()).Aggregate((f, s) => $"{f},{s}")}|{logsQueryResult.Table.Rows.Select(r => r.GetInt32(2).ToString()).Aggregate((f, s) => $"{f},{s}")}")
                .chxl($"0:|{logsQueryResult.Table.Rows.Select(r => r.GetDateTimeOffset(0).Value.ToString("ddd", new CultureInfo("ru-RU"))).Aggregate((f, s) => $"{f}|{s}")}");

            return new Uri(chart.toURL());
        }

        public string GetReportQuery(long chatId) => $@"let chatId = ""{chatId}"";
            AppTraces
            | where ((OperationName == ""MemesFinderGateway""))
            | where Message startswith ""Update received: ""
            | extend tgUpdate = parse_json(replace_string(Message, ""Update received: "", """"))
            | where ((tostring(tgUpdate.message) != """" or tostring(tgUpdate.edited_message) != """") and (tostring(tgUpdate.message.chat.id) == chatId or tostring(tgUpdate.edited_message.chat.id) == chatId))
            | summarize NewMessageCount = countif(tostring(tgUpdate.message) != """"), EditedMessageCount = countif(tostring(tgUpdate.edited_message) != """") by bin(TimeGenerated, 1d)
            | order by TimeGenerated asc";

        public string GetReportText(TimeSpan reportPeriod)
            => $"Количество сообщений по дням\n\n#reports\n\n{DateTime.Now.Add(-reportPeriod).ToShortDateString()} - {DateTime.Now.ToShortDateString()}";
    }
}

