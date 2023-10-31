#pragma warning disable CS0168
//using System.Net.Mail;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using TodoApp.Models;
using System.Net;
using TodoApp.Settings;

namespace TodoApp.Services;

public class EmailService
{
    public async Task SendEmailAsync(User user)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(EmailSettings.Name, EmailSettings.Email));
        message.To.Add(MailboxAddress.Parse(user.Email));
        message.Subject = CreateAccountEmailModel.Subject;
        var builder = new BodyBuilder { TextBody ="", HtmlBody = CreateAccountEmailModel.HtmlBody };
        message.Body = builder.ToMessageBody();
        SendEmailDefault(message);
    }

    private async void SendEmailDefault(MimeMessage message) {
        try
        {
            var smtpClient = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            await smtpClient.ConnectAsync(EmailSettings.EnderecoServidor, EmailSettings.Port);
            await smtpClient.AuthenticateAsync(EmailSettings.Email, EmailSettings.SmtpKey);
            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);

        }
        catch (Exception exception)
        {
            throw new InvalidOperationException(exception.Message);
        }
    }


    public async Task SendEmailToEventAsync(User user)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(EmailSettings.Name, EmailSettings.Email));
        message.To.Add(MailboxAddress.Parse(user.Email));
        message.Subject = InfoEventEmailModel.Subject;
        var builder = new BodyBuilder { TextBody = "", HtmlBody = InfoEventEmailModel.HtmlBody };
        message.Body = builder.ToMessageBody();
        SendEmailDefault(message);
    }
}

// CÓDIGO QUE ENVIA E-MAIL CONSUMINDO API DA BREVO (AINDA N FUNFA)

/*public class EmailService : IEmailService
{
    private string HtmlBodyAccountCreate { get; set; } = "<html><head></head><body><p>Hello,</p>This is my first transactional email sent from Brevo.</p></body></html>";

    private Uri baseUri { get; init; } = new Uri("https://api.brevo.com/");

    private string endpoint { get; init; } = "v3/smtp/email";

    public async Task<bool> SendDefaultEmail(User user)
    {
        try
        {
            HttpClient client = new()
            {
                BaseAddress = baseUri
            };
            client.DefaultRequestHeaders.Add("api-key", EmailSettings.ApiKey);
            var content = new
            {
                sender = new
                {
                    name = EmailSettings.Name,
                    email = EmailSettings.Email,

                },
                to = new
                {
                    email = user.Email,
                    name = user.Name
                },
                subject = "Congragulations ! Your create account sucess !",
                htmlContent = HtmlBodyAccountCreate
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(this.endpoint, content);
            var message = response.Content.ToString;
            if (response.StatusCode == HttpStatusCode.Unauthorized) return false;
            return true;

        }
        catch (Exception exception)
        {
            return false;
        }
    }
}*/
