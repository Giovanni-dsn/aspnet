using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Repositories;
[ApiController]
[Route("/events")]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;

    public EventController(EventService service)
    {
        _eventService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var list = await _eventService.GetEventUserList(username);
        if (list.IsNullOrEmpty()) return Problem("This user doesn't have a event", statusCode: 404);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var Event = await _eventService.CreateEvent(username, request);
        if (Event == null) return Problem("Date cannot be before the current date", statusCode: 400);
        return Created("/afazer", Event);
    }
}