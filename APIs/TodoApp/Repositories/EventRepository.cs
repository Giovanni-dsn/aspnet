using Data;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
namespace TodoApp.Repositories;
public class EventRepository : IEventRepository
{
    private readonly AppDbContext Context;
    private readonly UserRepository UserRepository;

    public EventRepository(AppDbContext context, UserRepository userRepository)
    {
        Context = context;
        UserRepository = userRepository;
    }

    public async Task<Event> CreateEvent(string username, EventDto dto)
    {
        var user = await UserRepository.GetUserByUsername(username);
        var Event = new Event(dto.Title, dto.Date, user);
        await Context.Events.AddAsync(Event);
        await Context.SaveChangesAsync();
        return Event;
    }

    public async Task<IEnumerable<Event>> GetEventUserList(string username)
    {
        var user = await UserRepository.GetUserByUsername(username);
        var list = await Context.Events.Where(x => x.User.Id == user.Id).ToListAsync();
        return list;
    }
}