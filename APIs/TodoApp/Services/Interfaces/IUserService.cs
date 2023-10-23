using TodoApp.Dto;
using TodoApp.Models;

namespace TodoApp.Services;

public interface IUserService
{
    public Task<LoginDto?> Authenticate(string username, string password);
    public Task<User> Register(UserDto request);
}