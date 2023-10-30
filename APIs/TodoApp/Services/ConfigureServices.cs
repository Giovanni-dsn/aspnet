using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Repositories;

namespace TodoApp.Services;
public static class ConfigureServices
{
    public static void AddAllCustomersServices(this IServiceCollection Services)
    {
        Services.AddScoped<TodoRepository>();
        Services.AddScoped<UserRepository>();
        Services.AddScoped<EventRepository>();
        Services.AddScoped<TokenService>();
        Services.AddScoped<TodoService>();
        Services.AddScoped<UserService>();
        Services.AddScoped<EventService>();
        Services.AddScoped<EmailService>();
    }

    public static void AddJwt(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAuthorization(x =>
        {
            x.FallbackPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
        });
        Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtBearerSettings:SecretKey"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}