using System.Security.Claims;
using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using ApiPedidos.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Endpoints
{
    public class CategoryPut
    {
        public static string Template => "/categories/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] Guid id, CategoryRequestDto categoryRequest, AppDbContext context, HttpContext http)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                category.Name = categoryRequest.Name;
                category.Active = categoryRequest.Active;
                category.EditedBy = userId;
                category.EditedOn = DateTime.Now;
                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return Results.Ok();
            }
            return Results.NotFound();
        }
    }
}
