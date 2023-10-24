#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models;
public class Todo
{
    public Todo() { }
    public Todo(string title, bool done, string? description, User user)
    {
        Title = title;
        Done = done;
        User = user;
        if (description != null) Description = description;
    }
    public int Id { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }
    public bool Done { get; set; } = false;

    [StringLength(200, MinimumLength = 5, ErrorMessage = "Description must be bettwen 5 and 200 characters long")]
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now.Date;

    [ForeignKey("UserId")]
    [Required]
    public User User { get; set; }
}