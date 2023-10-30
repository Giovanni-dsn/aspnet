using TodoApp.Models;

namespace TodoApp.Services;

public interface IEmailService
{
    public Task SendEmailAsync(User user);
}