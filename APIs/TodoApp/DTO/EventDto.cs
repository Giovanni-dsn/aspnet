using System.ComponentModel.DataAnnotations;

public record EventDto
{
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }

    public EventDto(string title, DateTime date)
    {
        Title = title;
        Date = date;
    }

    [Required]
    public DateTime Date { get; set; }
}