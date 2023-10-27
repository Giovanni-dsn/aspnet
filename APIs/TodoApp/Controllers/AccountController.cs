using System.Security.Claims;
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
            var result = await UserService.Register(request);
            if (result == null) return Problem("Name is required to register", statusCode: 400);
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

    [HttpPut("edit")]

    public async Task<IActionResult> Put([FromBody] UserPutDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var result = await UserService.EditUser(username, request);
        if (result) return Ok("Edited");
        return Problem("invalid fields or unavailable username", statusCode: 400);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var result = await UserService.DeleteUser(username);
        if (result) return Ok("Deleted");
        return Problem();
    }
}