using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Identity;

namespace ApiPedidos.Endpoints.Users
{
    public class UserPost
    {
        public static string Template => "/Users";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(UserRequest userRequest, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser
            {
                UserName = userRequest.email,
                Email = userRequest.email,
                PhoneNumber = userRequest.phone
            };

            var userResult = userManager.CreateAsync(user, userRequest.password).Result;
            if (!userResult.Succeeded) return Results.BadRequest(userResult.Errors.First());
            return Results.Created($"/Users/{user.Id}", user.Id);
        }
    }
}
