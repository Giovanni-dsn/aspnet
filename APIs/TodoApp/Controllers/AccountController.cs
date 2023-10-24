using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Dto;
using TodoApp.Services;

[ApiController]
[Route("/account")]
public class AccountController : ControllerBase
{
    private readonly UserService UserService;
    public AccountController([FromServices] UserService userservice)
    {
        UserService = userservice;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var confirm = await UserService.CheckUsernameIsAvailable(request.Email);
        if (confirm)
        {
            await UserService.Register(request);
            return Created("account/login", request.Email);
        }
        return Problem("E-mail already registered", statusCode: 400);
    }

    [AllowAnonymous]
    [HttpGet("login")]

    public async Task<IActionResult> Login([FromBody] UserDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await UserService.Authenticate(request.Email, request.Password);
        if (user == null) return Problem("Email or password entered is invalid", statusCode: 400);
        return Ok(user);
    }
}