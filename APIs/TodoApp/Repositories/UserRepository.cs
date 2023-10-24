using Data;
using Microsoft.EntityFrameworkCore;
using TodoApp.Dto;
using TodoApp.Models;
namespace TodoApp.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext Context;

    public UserRepository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await Context.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<User?> CheckUserLogin(string username, string password)
    {
        return await Context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
    }

    public async Task<User> CreateUser(UserDto dto)
    {
        var user = new User(dto.Email, dto.Password);
        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> CheckUserExists(string username)
    {
        var Unavailable = await Context.Users.AnyAsync(user => user.Username == username);
        return !Unavailable;
    }
}