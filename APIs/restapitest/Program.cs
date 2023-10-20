using Microsoft.AspNetCore.Mvc;
using System.Linq;

// PRIMEIRA API REST
internal class Program
{
    private static void Main(string[] args)
    {
        List<ProductModel> ProductList = new();
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSqlServer<AppDbContext>(builder.Configuration["Database:SqlServer"]);
        var app = builder.Build();

        app.MapGet("/", () => "Bem vindo de volta Giovanni, essa é uma cópia da sua Primeira API, colocando-a para o modelo REST");

        app.MapGet("/user", () => new { name = "Giovanni  Novais", age = 17, content = "Estudando programação" });

        app.MapGet("/addheader", (HttpResponse response) => {
            response.Headers.Add("Teste", "Testando");
            return "Adicionado ao Header";
            });

        //exemplo.com/product/{valor}
        app.MapGet("/product/{code}", ([FromRoute] string code, AppDbContext context) => {
            var productReturn = context.Products.FirstOrDefault(a => a.Code == code);
            if (productReturn == null) {
                return Results.NotFound();
            }
            else {
                return Results.Ok(productReturn);
            }
        });

        app.MapGet("/productlist", (AppDbContext context) => {
            var list = context.Products.ToList();
            return list;
        });
        
        app.MapPost("/product", (ProductRequest request, AppDbContext context) => {
            var product = new ProductModel{
                Name = request.Name,
                Code = request.Code
            };
            context.Products.Add(product);
            context.SaveChanges();
            return Results.Created($"/product/{product.Id}", product);
        });

        //exemplo.com/product?code={valor}&newname={valor}
        app.MapPut("/product",([FromQuery] string code, [FromQuery] string newname, AppDbContext context) => {
            var productSaved = context.Products.FirstOrDefault(a => a.Code == code);
            if (productSaved != null) {
                productSaved.Name = newname;
                context.SaveChanges();
                return Results.Ok(productSaved);
            }
            return Results.NotFound();
        });

        app.MapDelete("/product/{code}", ([FromRoute] string code, AppDbContext context) => {
            var productSaved = context.Products.FirstOrDefault(a => a.Code == code);

            if (productSaved != null) {
                context.Products.Remove(productSaved);
                context.SaveChanges();
                return Results.Ok();
            }
            return Results.NotFound();
        });
        app.Run();
    }
}
