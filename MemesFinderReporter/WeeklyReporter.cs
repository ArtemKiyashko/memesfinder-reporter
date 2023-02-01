using System.Threading.Tasks;
using MemesFinderReporter.Interfaces.Reports;
using Microsoft.Azure.WebJobs;
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

        [FunctionName("weeklyreports")]
        public async Task RunWeeklyReports([TimerTrigger("%WeeklyReporterSchedule%",
        #if DEBUG
            RunOnStartup = true
        #endif
            )]TimerInfo myTimer, ILogger log)
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

