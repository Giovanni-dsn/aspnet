using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;
public class User
{
    [Key]
    public int Id { get; init; }
    public string Username { get; init; }

    [EmailAddress]
    [Required]
    public string Email { get; init; }

    [MinLength(4)]
    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; } = "user";

    public User(string email, string password, string? role)
    {
        Username = email;
        Email = email;
        Password = password;
        if (role != null) Role = role;
    }
};
