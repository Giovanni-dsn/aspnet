using Microsoft.AspNetCore.Mvc;
using System.Linq;

// PRIMEIRA API 
internal class Program
{
    private static void Main(string[] args)
    {
        // TREINANDO CRIAÇÃO DE ENDPOINTS
        
        List<ProductModel> ProductList = new();
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Bem vindo de volta Giovanni, essa é sua primeira API");
        app.MapGet("/user", () => new { name = "Giovanni  Novais", age = 17, content = "Estudando programação" });

        app.MapGet("/addheader", (HttpResponse response) => {
            response.Headers.Add("Teste", "Testando");
            return "Adicionado ao Header";
            });
        
        //site.com/getproduct?codproduct={valor}
        app.MapGet("/getproduct", ([FromQuery] string codeproduct) => {
            return ProductList.FirstOrDefault(a => a.Code == codeproduct);
        });

        //site.com/getproduct/{valor}
        app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
            return ProductList.FirstOrDefault(a => a.Code == code);
        });

        app.MapGet("/list", () => ProductList);

        app.MapGet("/getproductbyheader", (HttpRequest request) => {
            return request.Headers["code"].ToString();
        });
        
        app.MapPost("/addproduct", (ProductModel product) => {
            ProductList.Add(product);
            return "Sucess registered";
        });

        app.MapPut("/editnameproduct",([FromQuery] string code, [FromQuery] string newname) => {
            var productSaved = ProductList.FirstOrDefault(a => a.Code == code);
            if (productSaved != null) {
                productSaved.Name = newname;
                return "Sucess";
            }
            return "Not Found !";
        });

        app.MapDelete("/removeproduct/{code}", ([FromRoute] string code) => {
            if (ProductList.Any(a => a.Code == code)) {
                var productSaved = ProductList.First(a => a.Code == code);
                ProductList.Remove(productSaved);
                return "Successfully removed";
            }
            return "Product not found !";
        });
        app.Run();
    }
}


public class ProductModel {
    public string? Name {get; set;}
    public string? Code {get; set;}
}