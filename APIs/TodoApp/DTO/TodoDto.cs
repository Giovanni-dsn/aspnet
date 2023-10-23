using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dto;

public class TodoDto
{
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }
    public bool Done { get; set; } = false;
    public TodoDto(string title, bool done)
    {
        Title = title;
        if (done) { Done = done; }
    }
}