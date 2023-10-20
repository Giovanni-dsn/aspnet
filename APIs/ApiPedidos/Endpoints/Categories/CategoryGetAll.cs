using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using ApiPedidos.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPedidos.Endpoints
{
    public class CategoryGetAll
    {
        public static string Template => "/categorieslist";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action(AppDbContext context)
        {
            var categorylist = await context.Categories.ToListAsync();
            var response = categorylist.Select(x => new CategoryResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Active = x.Active
            });
            return Results.Ok(response);
        }
    }
}
