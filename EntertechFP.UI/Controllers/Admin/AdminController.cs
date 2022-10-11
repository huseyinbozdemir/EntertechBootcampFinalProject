using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntertechFP.UI.Controllers.Admin
{
    [Authorize(Roles="Admin")]
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
            if (HttpContext.Request.Cookies["adm_session"] is not null)
                return RedirectToAction("Index");
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
                CookieHelper cookieHelper = new CookieHelper();
                cookieHelper.SignIn(claims, model.RememberMe, this);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
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
