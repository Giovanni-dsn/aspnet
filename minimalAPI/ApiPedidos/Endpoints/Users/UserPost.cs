using System.Security.Claims;
using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Identity;

namespace ApiPedidos.Endpoints.Users
{
    public class UserPost
    {
        public static string Template => "/users";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(UserRequest userRequest, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser
            {
                UserName = userRequest.Email,
                Email = userRequest.Email,
                PhoneNumber = userRequest.Phone
            };

            var userResult = userManager.CreateAsync(user, userRequest.Password).Result;
            if (!userResult.Succeeded) return Results.BadRequest(userResult.Errors.First());
            var claimResult = userManager.AddClaimAsync(user, new Claim("Name", userRequest.Name)).Result;
            if (!claimResult.Succeeded) return Results.BadRequest(claimResult.Errors.First());
            return Results.Created($"/Users/{user.Id}", user.Id);
        }
    }
}
