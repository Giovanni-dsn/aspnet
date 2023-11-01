#pragma warning disable CS8618
namespace TodoApp.Models;

public abstract class EmailModel
{
    public string Subject { get; init; }
    public string HtmlBody { get; init; }
}