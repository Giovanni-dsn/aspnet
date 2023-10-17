using TodoApp.Models;

namespace TodoApp.Dto;

public record UserAuthenticated(User User, string Token);