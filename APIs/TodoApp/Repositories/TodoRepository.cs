using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Dto;
using TodoApp.Models;

namespace TodoApp.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext Context;

    public TodoRepository([FromServices] AppDbContext context)
    {
        Context = context;
    }

    public async Task<List<Todo>> GetTodoListUser(string username)
    {
        return await Context.Todos.Where(todo => todo.User.Username == username).ToListAsync();
    }
    public async Task<Todo?> GetById(int id)
    {
        var todo = await Context.Todos.FirstOrDefaultAsync(x => x.Id == id);
        return todo;
    }

    public async Task<Todo> CreateTodo(TodoDto request, User user)
    {
        Todo todo = new(request.Title, request.Done, user);
        await Context.Todos.AddAsync(todo);
        await Context.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> UpdateTodo(int id, TodoDto request)
    {
        var todoSaved = await Context.Todos.FirstAsync(x => x.Id == id);
        todoSaved.Title = request.Title;
        todoSaved.Done = request.Done;
        Context.Update(todoSaved);
        await Context.SaveChangesAsync();
        return todoSaved;
    }

    public async void DeleteTodo(int id)
    {
        var todoSaved = await Context.Todos.FirstAsync(x => x.Id == id);
        Context.Remove(todoSaved);
        await Context.SaveChangesAsync();
    }
}