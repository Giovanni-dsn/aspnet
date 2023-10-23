using TodoApp.Models;

namespace TodoApp.Dto;

public record LoginDto(User User, string Token);