using TodoApp.Dto;
using TodoApp.Models;
namespace TodoApp.Repositories;
public interface ITodoRepository
{
    public Task<List<Todo>> GetTodoListUser(string id);

    public Task<Todo?> GetById(int id);

    public Task<Todo> CreateTodo(TodoDto request, User user);

    public Task<Todo> UpdateTodo(int id, TodoDto request);

    public void DeleteTodo(int id);

}