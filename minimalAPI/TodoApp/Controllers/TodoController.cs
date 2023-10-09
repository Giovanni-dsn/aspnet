using Microsoft.AspNetCore.Mvc;
using Data;

[ApiController]
[Route("[Controller]")]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("/[Controller]list")]
    public IResult Get([FromServices] AppDbContext context)
    {
        var listTodo = context.Todos.ToList();
        return Results.Ok(listTodo);
    }

    [HttpGet]
    [Route("/[Controller]/{id}")]
    public IResult GetById([FromRoute] int id, AppDbContext context)
    {
        var todo = context.Todos.FirstOrDefault(x => x.Id == id);
        if (todo == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(todo);
    }

    [HttpPost]
    public IResult Post([FromServices] AppDbContext context, [FromBody] Todo request)
    {
        if (request != null)
        {
            context.Todos.Add(request);
            context.SaveChanges();
            return Results.Created($"/Todo/{request.Id}", request);
        }
        return Results.BadRequest();
    }

    [HttpPut]
    [Route("{id}")]
    public IResult Put([FromRoute] int id, [FromServices] AppDbContext context, [FromBody] Todo request)
    {
        var todoSaved = context.Todos.FirstOrDefault(x => x.Id == id);
        if (todoSaved == null)
        {
            return Results.NotFound();
        }
        todoSaved.Title = request.Title;
        context.Update(todoSaved);
        context.SaveChanges();
        return Results.Ok(todoSaved);
    }

    [HttpDelete]
    [Route("{id}")]
    public IResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var todoSaved = context.Todos.FirstOrDefault(x => x.Id == id);
        if (todoSaved == null)
        {
            return Results.NotFound();
        }
        context.Todos.Remove(todoSaved);
        context.SaveChanges();
        return Results.Ok("Removed");
    }
}