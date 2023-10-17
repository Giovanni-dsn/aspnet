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
    public UserAuthenticated? Authenticate(string username, string password, [FromServices] AppDbContext context, IConfiguration configuration)
    {
        var user = context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
        if (user == null) return null;
        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerSettings:SecretKey"]!);
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
        return new UserAuthenticated(user, tokenHandler.WriteToken(token));
    }
}