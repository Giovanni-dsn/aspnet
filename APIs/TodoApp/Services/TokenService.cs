using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Models;

namespace TodoApp.Services;
public class TokenService : ITokenService
{
    public TokenService(IConfiguration configuration)
    {
        Key = Encoding.ASCII.GetBytes(configuration["JwtBearerSettings:SecretKey"]!);
    }

    private byte[] Key { get; set; }

    public string JwtTokenGenerator(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
            new(ClaimTypes.Email, user.Username),
          }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "TodoAppIssuer"
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}