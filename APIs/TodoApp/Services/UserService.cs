using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Dto;
using TodoApp.Models;
using TodoApp.Repositories;
namespace TodoApp.Services;

public class UserService : IUserService
{
    private readonly UserRepository Repository;
    private readonly TokenService TokenService;

    public UserService([FromServices] UserRepository repository, [FromServices] TokenService tokenService)
    {
        Repository = repository;
        TokenService = tokenService;
    }
    public async Task<LoginDto?> Authenticate(string username, string password)
    {
        var user = await Repository.CheckUserLogin(username, password);
        if (user == null) return null;
        var token = TokenService.JwtTokenGenerator(user);
        user.Password = string.Empty;
        return new LoginDto(user, token);
    }

    public async Task<User> Register(UserDto request)
    {
        return await Repository.CreateUser(request);
    }

    public async Task<bool> CheckUsernameIsAvailable(string username)
    {
        return await Repository.CheckUserExists(username);
    }
}