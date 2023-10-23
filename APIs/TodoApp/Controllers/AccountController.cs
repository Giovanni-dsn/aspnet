using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Dto;
using TodoApp.Models;
using TodoApp.Repositories;
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
        await UserService.Register(request);
        return Ok(request.Email);
    }

    [AllowAnonymous]
    [HttpGet("login")]

    public async Task<IActionResult> Login([FromBody] User request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await UserService.Authenticate(request.Username, request.Password);
        return Ok(user);
    }
}