using System.Security.Claims;
using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using ApiPedidos.Dto;
using Microsoft.AspNetCore.Authorization;

namespace ApiPedidos.Endpoints
{
    public class CategoryPost
    {
        public static string Template => "/categories";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action(CategoryRequestDto categoryRequest, AppDbContext context, HttpContext http)
        {
            var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var category = new Category(categoryRequest.Name, userId, userId);
            if (category.IsValid)
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Results.Created($"/categories/{category.Id}", category.Id);
            }
            return Results.BadRequest(category.Notifications);
        }
    }
}
