using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

[ApiController]
[Route("/account")]
public class AccountController : ControllerBase
{
    private AppDbContext Context { get; init; }
    private IConfiguration Configuration { get; init; }
    public AccountController([FromServices] AppDbContext context, IConfiguration configuration)
    { Context = context; Configuration = configuration; }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] User request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Context.Users.Add(request);
        Context.SaveChanges();
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("login")]

    public IActionResult Login([FromBody] User request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        UserService userService = new(Context, Configuration);
        var user = userService.Authenticate(request.Username, request.Password);
        return Ok(user);
    }
}