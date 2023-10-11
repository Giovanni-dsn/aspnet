using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Endpoints
{
    public class CategoryDelete
    {
        public static string Template => "/categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] Guid id, AppDbContext context)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return Results.Ok();
            }
            return Results.NotFound();
        }
    }
}
