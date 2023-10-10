using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Endpoints.Categories
{
    public class CategoryGet
    {
        public static string Template => "/categories/{name}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] string name, AppDbContext context)
        {
            var category = context.Categories.FirstOrDefault(x => x.Name == name);
            if (category != null)
            {
                var response = new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                    Active = category.Active
                };
                return Results.Ok(response);
            }
            return Results.NotFound();

        }
    }
}
