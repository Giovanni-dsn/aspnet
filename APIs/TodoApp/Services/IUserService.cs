using TodoApp.Dto;

namespace TodoApp.Services;

public interface IUserService
{
    public UserLoginDto? Authenticate(string username, string password);
}