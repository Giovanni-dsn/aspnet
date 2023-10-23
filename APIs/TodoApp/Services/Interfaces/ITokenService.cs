using TodoApp.Models;

public interface ITokenService
{
    public string JwtTokenGenerator(User user);
}