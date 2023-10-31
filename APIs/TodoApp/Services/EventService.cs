using Microsoft.IdentityModel.Tokens;
using TodoApp.Models;
using TodoApp.Repositories;
namespace TodoApp.Services;
public class EventService : IEventService
{

    private readonly EventRepository Repository;

    private readonly UserRepository UserRepository;

    public EventService(EventRepository eventRepository, UserRepository userRepository)
    {
        Repository = eventRepository;
        UserRepository = userRepository;
    }

    public async Task<Event?> CreateEvent(string username, EventDto dto)
    {
        if (dto.Date < DateTime.Now)
        {
            return null;
        }
        return await Repository.CreateEvent(username, dto);
    }

    public async Task<IEnumerable<Event>> GetEventUserList(string username)
    {
        return await Repository.GetEventUserList(username);
    }

    public async Task<bool> CheckPermission(int id, string username)
    {
        var list = await Repository.GetEventUserList(username);
        if (list.IsNullOrEmpty()) return false;
        if (list.Any(todo => todo.Id == id)) return true; else return false;

    }

    public async Task<Event?> EditEvent(int id, EventDto dto)
    {
        if (dto.Date < DateTime.Now) return null;
        return await Repository.UpdateEvent(id, dto);
    }

    public async Task<bool> RemoveEvent(int id)
    {
        return await Repository.DeleteEvent(id);
    }

    public async Task<IEnumerable<Event>> GetEventsCurrentDate()
    {
        return await Repository.GetEventsCurrentDate();
    }

    public async Task<bool> CheckExistsTodayEvents()
    {
        return await Repository.CheckExistsTodayEvents();
    }
}