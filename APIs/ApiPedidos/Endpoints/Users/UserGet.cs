using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using ApiPedidos.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Endpoints.Users
{
    public class UserGet
    {
        public static string Template => "/Users/{email}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] string email, UserManager<IdentityUser> userManager)
        {
            var userSaved = await userManager.FindByNameAsync(email);
            if (userSaved != null)
            {
                var claims = await userManager.GetClaimsAsync(userSaved);
                var userClaimName = claims.First(x => x.Type == "Name")!;
                var response = new UserResponseDto(userSaved.UserName, userSaved.PhoneNumber, userClaimName.Value);
                return Results.Ok(response);
            }
            return Results.NotFound();
        }
    }
}
