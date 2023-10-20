using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Endpoints
{
    public class CategoryDelete
    {
        public static string Template => "/categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] Guid id, AppDbContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Results.Ok();
            }
            return Results.NotFound();
        }
    }
}
