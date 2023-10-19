using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Dto;
using TodoApp.Models;
namespace TodoApp.Services;

public class UserService
{
    private readonly AppDbContext Context;
    private readonly IConfiguration Configuration;

    public UserService(AppDbContext context, IConfiguration configuration) { Context = context; Configuration = configuration; }
    public UserLoginDto? Authenticate(string username, string password)
    {
        var user = Context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
        if (user == null) return null;
        var key = Encoding.ASCII.GetBytes(Configuration["JwtBearerSettings:SecretKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Email, username),
            new Claim("Role", user.Role)
          }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "TodoAppIssuer"
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Password = string.Empty;
        return new UserLoginDto(user, tokenHandler.WriteToken(token));
    }
}