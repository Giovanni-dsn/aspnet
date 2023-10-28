using Microsoft.IdentityModel.Tokens;
using TodoApp.Models;
using TodoApp.Repositories;

public class EventService : IEventService
{

    private readonly EventRepository Repository;

    public EventService(EventRepository eventRepository)
    {
        Repository = eventRepository;
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
}