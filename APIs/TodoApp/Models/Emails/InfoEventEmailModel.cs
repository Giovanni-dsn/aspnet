namespace TodoApp.Models;

public class InfoEventEmailModel : EmailModel
{
    public InfoEventEmailModel(Event Event)
    {
        this.Subject = "You have an event scheduled for today";
        this.HtmlBody = $"<html><head></head><body><p>Hello {Event.User.Name}!<br>Your event \"{Event.Title}\" is scheduled for today at {Event.Date.Hour}:{Event.Date.Minute}.<br><strong>P.S: This is a transactional email and should not be responded to</strong></p></body></html>";
    }
}