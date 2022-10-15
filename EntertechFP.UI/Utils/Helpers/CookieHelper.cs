using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Utils.Helpers
{
    public class CookieHelper
    {
        public async void SignIn(List<Claim> claims, bool remember, HttpContext context, string scheme)
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProps = new AuthenticationProperties() { IsPersistent = true };
            if (remember)
                authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15);
            else
                authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1);
            await context.SignInAsync(scheme, new ClaimsPrincipal(claimsIdentity), authProps);
        }

        public async void SignOut(HttpContext context, string scheme)
        {
            await context.SignOutAsync(scheme);
        }
    }
}
