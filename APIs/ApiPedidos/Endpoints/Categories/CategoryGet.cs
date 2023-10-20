using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using ApiPedidos.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Endpoints.Categories
{
    public class CategoryGet
    {
        public static string Template => "/categories/{name}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] string name, AppDbContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Name == name);
            if (category != null)
            {
                var response = new CategoryResponseDto
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
