namespace TodoApp.Models;

public class CreateAccountEmailModel : EmailModel
{
    public CreateAccountEmailModel(User user)
    {
        this.Subject = "Congragulations ! Your account create sucess.";
        this.HtmlBody = $"<html><head></head><body><p>Hello {user.Name}!<br>your account has been created successfully. Welcome to DailyApp ! Have a good {DateTime.Now.DayOfWeek}<br><strong>P.S: This is a transactional email and should not be responded to</strong</p></body></html>";
    }
}