using EntertechFP.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace EntertechFP.UI.Controllers.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IConfiguration configuration;

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private string GetConfigurationInfo(string key)
        {
            var value = configuration.GetSection(key).Get<string>();
            return value;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(AdminViewModel model)
        {
            string username = GetConfigurationInfo("Admin:username");
            string password = GetConfigurationInfo("Admin:password");
            if (username.Equals(model.UserName) && password.Equals(model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Admin"),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties();
                if (model.RememberMe is not null)
                    authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15);
                else
                    authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Hatalı kullanıcı adı veya şifre.";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
