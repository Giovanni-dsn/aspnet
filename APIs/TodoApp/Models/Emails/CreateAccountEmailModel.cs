namespace TodoApp.Models;

public static class CreateAccountEmailModel 
{
    public static string Subject = "Congragulations ! Your account create sucess.";
    public static string HtmlBody = "<html><head></head><body><p>Hello ###!<br>your account has been created successfully. Welcome to DailyApp<br><strong>P.S: This is a transactional email and should not be responded to</strong</p></body></html>"
}