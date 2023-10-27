using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dto;
public record UserDto
{
    [EmailAddress]
    [MinLength(5, ErrorMessage = "Minimum length of 5 characters")]
    public required string Email { get; set; }

    [MinLength(4, ErrorMessage = "Minimum length of 4 characters")]
    public required string Password { get; set; }

    [StringLength(maximumLength: 30, MinimumLength = 3)]
    public string? Name { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}