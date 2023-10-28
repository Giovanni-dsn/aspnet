using TodoApp.Models;

public interface IEventRepository
{
    public Task<Event> CreateEvent(string username, EventDto dto);
    public Task<IEnumerable<Event>> GetEventUserList(string username);
}