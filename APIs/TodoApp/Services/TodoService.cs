using Microsoft.IdentityModel.Tokens;
using TodoApp.Repositories;

public class TodoService
{
    private readonly TodoRepository Repository;

    public TodoService(TodoRepository repository)
    {
        Repository = repository;
    }

    public async Task<byte> CheckPermission(int id, string username)
    {
        var list = await Repository.GetTodoListUser(username);
        if (list.IsNullOrEmpty()) return 3;
        if (list.Any(todo => todo.Id == id)) return 1; else return 0;

    }
}