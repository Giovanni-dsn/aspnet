using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using ApiPedidos.Endpoints;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration["ConnectionStrings:ApiPedidos"]);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);

app.Run();