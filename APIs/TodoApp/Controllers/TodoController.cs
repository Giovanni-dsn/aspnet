using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Dto;
using TodoApp.Repositories;
using System.Security.Claims;

[ApiController]
[Route("[Controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoRepository Repository;
    private readonly TodoService TodoService;
    private readonly UserRepository UserRepository;
    public TodoController([FromServices] TodoRepository _todoRepository, [FromServices] UserRepository _userRepository, [FromServices] TodoService _todoService)
    {
        Repository = _todoRepository;
        UserRepository = _userRepository;
        TodoService = _todoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var username = User.FindFirstValue(ClaimTypes.Email);
        if (username != null) return Ok(await Repository.GetTodoListUser(username));
        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var todo = await Repository.GetById(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TodoDto request)
    {
        if (ModelState.IsValid)
        {
            var username = User.FindFirst(ClaimTypes.Email)!.Value;
            var userTodo = await UserRepository.GetUserByUsername(username);
            var result = await Repository.CreateTodo(request, userTodo!);
            return Created($"/Todo/{result.Id}", new { Id = result.Id, Content = new TodoDto(result.Title, result.Done), UserId = result.User.Id });
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
            if (confirm == 1)
            {
                var result = await Repository.UpdateTodo(id, request);
                return Ok(result);
            }
            if (confirm == 2) return Unauthorized();
            return Problem("This user does not have a task", default, 202);
        }
        return BadRequest(ModelState);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var confirm = await TodoService.CheckPermission(id, username);
        if (confirm == 1)
        {
            Repository.DeleteTodo(id);
            return Ok("Removed");
        }
        if (confirm == 0) return Unauthorized();
        return Problem("This user does not have a task", default, 202);
    }
}