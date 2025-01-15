using RentACar.Global.User;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain.Store;
using Domain.Store.Enum;
using BL.Store;
using AutoMapper;

namespace UI.Ecommerce.Extensions
{
    public class IdentityHelper
    {
        public async static void Login(UserDO user, HttpContext context,IMapper mapper)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()));
            claims.Add(new Claim("User.ConfirmState", user.VerifyState == (int)VerifyStateEnum.Verified ? "true" : "false"));
            CartBL cartBL = new CartBL(mapper);
            CartDO cart;
            cart= cartBL.GetByUserId(user.Id);
            if (cart == null)
            {
                cart = cartBL.NewCart(user.Id);
            }
            if (user.VerifyState == (int)VerifyStateEnum.Unverified)
            {
                claims.Add(new Claim("User.ConfirmCode", user.VerifyConfirmCode));
            }
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties();
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }

        public async static void Logout(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
