using ApiPedidos.Data;
using ApiPedidos.Domain.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Endpoints.Users
{
    public class UserGet
    {
        public static string Template => "/Users/{email}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] string email, UserManager<IdentityUser> userManager)
        {
            var userSaved = userManager.FindByNameAsync(email).Result;
            if (userSaved != null)
            {
                var userClaims = userManager.GetClaimsAsync(userSaved).Result;
                var userClaimName = userClaims.FirstOrDefault(x => x.Type == "Name");
                var response = new UserResponse(userSaved.UserName, userSaved.PhoneNumber, userClaimName.Value);
                return Results.Ok(response);
            }
            return Results.NotFound();
        }
    }
}
