using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dto;
public record UserPutDto
{
    [EmailAddress]
    [MinLength(5, ErrorMessage = "Minimum length of 5 characters")]
    public string? Email { get; set; }

    [MinLength(4, ErrorMessage = "Minimum length of 4 characters")]
    public string? Password { get; set; }
}