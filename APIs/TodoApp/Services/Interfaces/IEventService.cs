using TodoApp.Models;

public interface IEventService
{
    public Task<Event?> CreateEvent(string username, EventDto dto);
    public Task<IEnumerable<Event>> GetEventUserList(string username);
    public Task<bool> CheckPermission(int id, string username);
    public Task<Event?> EditEvent(int id, EventDto dto);
    public Task<bool> RemoveEvent(int id);
}