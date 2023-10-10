using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Endpoints
{
    public class CategoryGetAll
    {
        public static string Template => "/categorieslist";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(AppDbContext context)
        {
            var categorylist = context.Categories.ToList();
            var response = categorylist.Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Active = x.Active
            });
            return Results.Ok(response);
        }
    }
}
