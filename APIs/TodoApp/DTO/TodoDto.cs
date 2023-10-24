using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dto;

public record TodoDto
{
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }
    public bool Done { get; set; } = false;

    [StringLength(200, MinimumLength = 5, ErrorMessage = "Description must be bettwen 5 and 200 characters long")]
    public string? Description { get; set; }
    public TodoDto(string title, bool done, string? description)
    {
        Title = title;
        if (done) { Done = done; }
        if (description != null) Description = description;
    }
}