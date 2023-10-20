using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;
public class Todo
{
    public Todo(string title)
    {
        Title = title;
    }
    public int Id { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }
    public bool Done { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.Now.Date;
}