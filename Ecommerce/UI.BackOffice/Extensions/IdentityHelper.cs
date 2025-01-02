using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain.Backoffice;
using Domain.Backoffice.Enum;

namespace UI.BackOffice.Extensions
{
    public class IdentityHelper
    {
        public async static void Login(UserDO user, HttpContext context)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()));
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
