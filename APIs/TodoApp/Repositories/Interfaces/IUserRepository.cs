using TodoApp.Dto;
using TodoApp.Models;
namespace TodoApp.Repositories;
public interface IUserRepository
{
    public Task<User?> GetUserByUsername(string username);
    public Task<User?> CheckUserLogin(string username, string password);
    public Task<User> CreateUser(UserDto dto);
}