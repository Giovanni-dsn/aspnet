#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TodoApp.Models;
public class Todo
{
    public Todo() { }
    public Todo(string title, bool done, User user)
    {
        Title = title;
        Done = done;
        User = user;
    }
    public int Id { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "At least 3 characters for Title")]
    public string Title { get; set; }
    public bool Done { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.Now.Date;
    [ForeignKey("UserId")]
    [Required]
    public User User { get; set; }
}