using EntertechFP.UI.Models.Entitities;
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
        private readonly CookieHelper cookieHelper;
        private readonly RequestHelper requestHelper;
        public AdminController(IConfiguration configuration, CookieHelper cookieHelper, RequestHelper requestHelper)
        {
            this.configuration = configuration;
            this.cookieHelper = cookieHelper;
            this.requestHelper = requestHelper;
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
        public IActionResult Login(LoginViewModel model)
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
        public IActionResult Logout()
        {
            cookieHelper.SignOut(this);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("PendingEvents");
        }
        [HttpGet]
        public IActionResult PendingEvents()
        {
            var model = requestHelper.Action<List<EventDto>>("event/?include=0&pending=1", ActionType.Get, null);
            var result = model.Result.Data;
            return View(result);
        }
    }
}
