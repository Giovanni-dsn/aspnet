using Microsoft.AspNetCore.Mvc;
using Data;
using TodoApp.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[Controller]")]
public class TodoController : ControllerBase
{
    private AppDbContext Context { get; }
    public TodoController([FromServices] AppDbContext _context) { Context = _context; }

    [HttpGet]
    [Route("/[Controller]list")]
    public IResult Get() => Results.Ok(Context.Todos.ToList());

    [HttpGet]
    [Route("/[Controller]/{id}")]
    public IResult GetById([FromRoute] int id)
    {
        var todo = Context.Todos.FirstOrDefault(x => x.Id == id);
        if (todo == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(todo);
    }

    [HttpPost]
    [Authorize("Admin")]
    public IResult Post([FromBody] Todo request)
    {
        if (ModelState.IsValid)
        {
            Context.Todos.Add(request);
            Context.SaveChanges();
            return Results.Created($"/Todo/{request.Id}", request);
        }
        return Results.BadRequest(ModelState);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize("admin")]
    public IResult Put([FromRoute] int id, [FromBody] Todo request)
    {
        var todoSaved = Context.Todos.FirstOrDefault(x => x.Id == id);
        if (todoSaved == null)
        {
            return Results.NotFound();
        }
        todoSaved.Title = request.Title;
        Context.Update(todoSaved);
        Context.SaveChanges();
        return Results.Ok(todoSaved);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize("admin")]
    public IResult Delete([FromRoute] int id)
    {
        var todoSaved = Context.Todos.FirstOrDefault(x => x.Id == id);
        if (todoSaved == null)
        {
            return Results.NotFound();
        }
        Context.Todos.Remove(todoSaved);
        Context.SaveChanges();
        return Results.Ok("Removed");
    }
}