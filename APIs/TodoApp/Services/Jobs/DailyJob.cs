using Hangfire;
using TodoApp.Models;

namespace TodoApp.Services;

public class DailyJob
{
    public static bool Executing = false;
    private readonly EmailService EmailService;
    private readonly EventService EventService;
    private readonly UserService UserService;

    public DailyJob(EmailService emailService, EventService eventService, UserService userService)
    {
        EmailService = emailService;
        EventService = eventService;
        UserService = userService;
    }

    public async Task<bool> CheckDateToEvents()
    {
        var confirm = await EventService.CheckExistsTodayEvents();
        if (confirm)
        {
            var todayEvents = await EventService.GetEventsCurrentDate();
            foreach (Event Event in todayEvents)
            {
                var user = await UserService.GetUserByEvent(Event);
                await EmailService.SendEmailAsync(user, new InfoEventEmailModel(Event));
            }
            return true;
        }
        return false;
    }

    public void ExecuteJob()
    {
        DailyJob.Executing = true;
        RecurringJob.AddOrUpdate<DailyJob>("Job Warning", x => x.CheckDateToEvents(), Cron.Daily);
    }
    public void TestJob()
    {
        Console.WriteLine("------------- JOB funcionando");
    }
}