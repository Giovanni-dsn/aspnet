using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Dto;
using TodoApp.Repositories;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[Controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoRepository Repository;
    private readonly TodoService TodoService;
    private readonly UserRepository UserRepository;
    public TodoController(TodoRepository _todoRepository, UserRepository _userRepository, TodoService _todoService)
    {
        Repository = _todoRepository;
        UserRepository = _userRepository;
        TodoService = _todoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var username = User.FindFirstValue(ClaimTypes.Email);
        var todoList = await Repository.GetTodoListUser(username!);
        if (todoList.IsNullOrEmpty()) return Problem("This user doesn't have a task", statusCode: 202);
        return Ok(todoList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var permission = await TodoService.CheckPermission(id, username);
        if (permission)
        {
            var todo = await Repository.GetById(id);
            return Ok(todo);
        }
        return Unauthorized();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TodoDto request)
    {
        if (ModelState.IsValid)
        {
            var username = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await UserRepository.GetUserByUsername(username);
            var todo = await Repository.CreateTodo(request, user);
            return Created($"/Todo/{todo.Id}", todo);
        }
        return BadRequest(ModelState);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TodoDto request)
    {
        if (ModelState.IsValid)
        {
            var username = User.FindFirstValue(ClaimTypes.Email)!;
            var confirm = await TodoService.CheckPermission(id, username);
            if (confirm)
            {
                var result = await Repository.UpdateTodo(id, request);
                return Ok(result);
            }
            return Unauthorized();
        }
        return BadRequest(ModelState);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var confirm = await TodoService.CheckPermission(id, username);
        if (confirm)
        {
            Repository.DeleteTodo(id);
            return Ok("Removed");
        }
        return Unauthorized();
    }
}