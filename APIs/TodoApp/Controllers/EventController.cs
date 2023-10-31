using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Services;
[ApiController]
[Route("/events")]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;
    private readonly DailyJob dailyJob;

    public EventController(EventService service, DailyJob dailyJob)
    {
        _eventService = service;
        this.dailyJob = dailyJob;
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
        var permission = DailyJob.Executing;
        if (!permission)
            dailyJob.ExecuteJob();
        return Created("/afazer", Event);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Put(int id, EventDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var allowed = await _eventService.CheckPermission(id, username);
        if (!allowed) return Unauthorized();
        var Event = await _eventService.EditEvent(id, request);
        if (Event == null) return Problem("Invalid Date or Event Not Found", statusCode: 404);
        return Ok(Event);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var username = User.FindFirstValue(ClaimTypes.Email)!;
        var allowed = await _eventService.CheckPermission(id, username);
        if (!allowed) return Unauthorized();
        var removed = await _eventService.RemoveEvent(id);
        if (!removed) return NotFound();
        return Ok("Removed");

    }
}