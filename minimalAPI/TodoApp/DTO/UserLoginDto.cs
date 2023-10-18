using TodoApp.Models;

namespace TodoApp.Dto;

public record UserLoginDto(User User, string Token);