using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Endpoints
{
    public class CategoryPut
    {
        public static string Template => "/categories/{id}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, AppDbContext context)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                category.Name = categoryRequest.Name;
                category.Active = categoryRequest.Active;
                context.Categories.Update(category);
                context.SaveChanges();
                return Results.Ok();
            }
            return Results.NotFound();
        }
    }
}
