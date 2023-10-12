using ApiPedidos.Data;
using ApiPedidos.Domain.Products;

namespace ApiPedidos.Endpoints
{
    public class CategoryPost
    {
        public static string Template => "/categories";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(CategoryRequest categoryRequest, AppDbContext context)
        {
            var category = new Category(categoryRequest.Name, "Tester", "Editer");
            if (category.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return Results.Created($"/categories/{category.Id}", category.Id);
            }
            return Results.BadRequest(category.Notifications);
        }
    }
}
