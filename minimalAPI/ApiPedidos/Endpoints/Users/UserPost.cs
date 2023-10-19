using System.Security.Claims;
using ApiPedidos.Dto;
using Microsoft.AspNetCore.Identity;

namespace ApiPedidos.Endpoints.Users
{
    public class UserPost
    {
        public static string Template => "/users";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action(UserRequestDto userRequest, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser
            {
                UserName = userRequest.Email,
                Email = userRequest.Email,
                PhoneNumber = userRequest.Phone
            };

            var userResult = await userManager.CreateAsync(user, userRequest.Password);
            if (!userResult.Succeeded) return Results.BadRequest(userResult.Errors.First());
            var claimResult = await userManager.AddClaimAsync(user, new Claim("Name", userRequest.Name));
            if (!claimResult.Succeeded) return Results.BadRequest(claimResult.Errors.First());
            return Results.Created($"/Users/{user.Id}", user.Id);
        }
    }
}
