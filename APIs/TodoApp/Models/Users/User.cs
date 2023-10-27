#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;
public class User
{
    [Key]
    public int Id { get; init; }

    [StringLength(maximumLength: 30, MinimumLength = 3)]
    [Required]
    public string Name { get; set; }
    public string Username { get; set; }
    [Phone]
    public string PhoneNumber { get; set; } = "";

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    public bool EmailConfirmed { get; set; } = false;

    [MinLength(4)]
    [Required]
    public string Password { get; set; }

    public User() { } //Constructor empty for EF
    public User(string email, string password, string name, string? phone)
    {
        Username = email;
        Email = email;
        Password = password;
        Name = name;
        if (phone != null)
            PhoneNumber = phone;
    }
};
