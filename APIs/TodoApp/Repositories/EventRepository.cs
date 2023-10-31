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

    public async Task<IEnumerable<Event>> GetEventsCurrentDate()
    {
        var list = await Context.Events.Include(x => x.User).Where(x => x.Date.Day == DateTime.Now.Day && x.Date > DateTime.Now).ToListAsync();
        return list;
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

    public async Task<Event?> GetEventById(int id)
    {
        return await Context.Events.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Event?> UpdateEvent(int id, EventDto dto)
    {
        var Event = await GetEventById(id);
        if (Event == null) return null;
        Event.Title = dto.Title;
        Event.Date = dto.Date;
        Context.Update(Event);
        await Context.SaveChangesAsync();
        return Event;

    }

    public async Task<bool> DeleteEvent(int id)
    {
        var Event = await Context.Events.FirstAsync(x => x.Id == id);
        if (Event == null) return false;
        Context.Events.Remove(Event);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CheckExistsTodayEvents()
    {
        return await Context.Events.AnyAsync(x => x.Date.Day == DateTime.Now.Day);
    }
}