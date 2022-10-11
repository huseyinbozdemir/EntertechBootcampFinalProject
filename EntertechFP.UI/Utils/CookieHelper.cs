using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Utils
{
    public class CookieHelper
    {
        public async void SignIn(List<Claim> claims, bool remember, Controller controller)
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProps = new AuthenticationProperties() { IsPersistent = true };
            if (remember)
                authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15);
            else
                authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1);
            await controller.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
        }

        public async void SignOut(Controller controller)
        {
            await controller.HttpContext.SignOutAsync();
        }
    }
}
