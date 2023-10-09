using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<AppDbContext>("Server=localhost;Database=Books;User Id=sa;Password=Sqlserversenha123;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES");
var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.MapPost("/author", (AuthorRequest request, AppDbContext context) => {
    if (request != null) {
        var author = new Author(request.name, request.idade);
        context.Authors.Add(author);
        context.SaveChanges();
        return Results.Created($"/author/{author.Id}", author.Id);
    }
    return Results.BadRequest();
});

app.MapPost("/book", (BookRequest request, AppDbContext context) => {
    if (request != null) {
        if (context.Authors.Any(a => a.Id == request.authorId)) {
            var book = new Book (request.authorId, request.code, request.title, request.category);
            context.Books.Add(book);
            context.SaveChanges();
            return Results.Created($"/book/{book.Id}", book);
        }
        Results.Accepted("Indique um Id de um Autor existente !");
    }
    return Results.BadRequest();
});

app.MapGet("/book/{id}", ([FromRoute] int id, AppDbContext context) => {
    var book = context.Books.Include(p => p.Autor).FirstOrDefault(a => a.Id == id);
    if (book != null) {
        return Results.Ok(book);
    }
    return Results.NotFound();
});

app.MapGet("/author/{id}", ([FromRoute] int id, AppDbContext context) => {
    var author = context.Authors.FirstOrDefault(a => a.Id == id);
    if (author != null) {
        return Results.Ok(author);
    }
    return Results.NotFound();
});

app.MapPut("/author/{id}", ([FromRoute] int id, AuthorRequest request, AppDbContext context) => {
    if (request == null) {
        return Results.BadRequest();
    }
    else {
        var authorSaved = context.Authors.FirstOrDefault(a => a.Id == id);
        if (authorSaved != null) {
            if (request.name != null) authorSaved.Name = request.name;
            if (request.idade != 0) authorSaved.Idade = request.idade;
            context.SaveChanges();
            return Results.Ok(authorSaved);
        }
        return Results.NotFound();
    }
});

app.MapPut("/book/{id}", ([FromRoute] int id, BookRequest request, AppDbContext context) => {
    if (request == null) {
        return Results.BadRequest();
    }
    else {
        var book = context.Books.FirstOrDefault(a => a.Id == id);
        if (book != null) {
            if (request.code != null) book.Code = request.code;
            if (request.title != null) book.Title = request.title;
            if (request.authorId != 0) book.AuthorId = request.authorId;
            if (request.category != null) book.Category = request.category;
            context.SaveChanges();
            return Results.Ok(book);
        }
        return Results.NotFound();
    }
});

app.MapDelete("/author/{id}", ([FromRoute] int id, AppDbContext context) => {
    var author = context.Authors.FirstOrDefault(a => a.Id == id);
    if (author != null) {
        context.Authors.Remove(author);
        context.SaveChanges();
        Results.Ok("Successfully removed");
    }
    Results.NotFound();
});

app.MapDelete("/book/{id}", ([FromRoute] int id, AppDbContext context) => {
    var book = context.Books.FirstOrDefault(a => a.Id == id);
    if (book != null) {
        context.Books.Remove(book);
        context.SaveChanges();
        Results.Ok("Successfully removed");
    }
    Results.NotFound();
});

app.Run();
