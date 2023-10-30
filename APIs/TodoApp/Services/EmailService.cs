#pragma warning disable CS0168
//using System.Net.Mail;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using TodoApp.Models;
using System.Net;

namespace TodoApp.Services;
public class EmailService
{
    private string HtmlBodyAccountCreate { get; set; } = "<html><head></head><body><p>Hello,</p>This is my first transactional email sent from Brevo.</p></body></html>";
    public async Task SendEmailAsync(User user)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(EmailSettings.Name, EmailSettings.Email));
        message.To.Add(MailboxAddress.Parse(user.Email));
        message.Subject = "Congragulations ! Your create account sucess.";
        var builder = new BodyBuilder { TextBody = "It's my first email testing !", HtmlBody = HtmlBodyAccountCreate };
        message.Body = builder.ToMessageBody();
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
}


public static class EmailSettings
{
    public static readonly string Name = "DiaryApiContact";
    public static readonly string Email = "diaryapicontact@gmail.com";
    public static readonly string SmtpKey = "xsmtpsib-7ef1064439aee7a8de51c631e09ea40b792ebdf28eed60de558b5cd6fc680b94-ksZJNUBfWRhCm5P1";
    public static readonly string EnderecoServidor = "smtp-relay.brevo.com";
    public static readonly int Port = 587;

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
