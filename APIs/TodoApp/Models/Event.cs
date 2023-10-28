#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TodoApp.Models;
public class Event
{
    public int Id { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    public Event() { }
    public Event(string title, DateTime date, User user)
    {
        Title = title;
        Date = date;
        User = user;
    }
}