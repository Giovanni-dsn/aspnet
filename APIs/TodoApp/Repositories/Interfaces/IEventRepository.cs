using TodoApp.Models;

public interface IEventRepository
{
    public Task<Event> CreateEvent(string username, EventDto dto);
    public Task<IEnumerable<Event>> GetEventUserList(string username);
    public Task<Event?> GetEventById(int id);
    public Task<Event?> UpdateEvent(int id, EventDto dto);
    public Task<bool> DeleteEvent(int id);
}