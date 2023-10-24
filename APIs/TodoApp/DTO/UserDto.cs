using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dto;
public record UserDto
{
    [EmailAddress]
    [MinLength(5, ErrorMessage = "Minimum length of 5 characters")]
    public required string Email { get; set; }

    [MinLength(4, ErrorMessage = "Minimum length of 4 characters")]
    public required string Password { get; set; }
}