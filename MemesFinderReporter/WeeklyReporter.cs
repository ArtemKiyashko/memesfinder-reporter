using System.Threading.Tasks;
using MemesFinderReporter.Interfaces.Reports;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemesFinderReporter
{
    public class WeeklyReporter
    {
        private readonly IReportManager<IWeeklyReport> _weeklyReportManager;
        private readonly ITelegramBotClient _telegramBotClient;

        public WeeklyReporter(IReportManager<IWeeklyReport> weeklyReportManager, ITelegramBotClient telegramBotClient)
        {
            _weeklyReportManager = weeklyReportManager;
            _telegramBotClient = telegramBotClient;
        }

        [Function("weeklyreports")]
        public async Task RunWeeklyReports([TimerTrigger("%WeeklyReporterSchedule%")] TimerInfo myTimer)
        {
            var reports = await _weeklyReportManager.GetReportsResults();

            foreach(var report in reports)
            {
                await _telegramBotClient.SendPhotoAsync(
                    chatId: report.ChatId,
                    photo: new InputFileUrl(report.PictureUri),
                    messageThreadId: report.ThreadId,
                    caption: report.Text);
            }
        }
    }
}

