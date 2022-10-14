using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntertechFP.UI.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly CookieHelper cookieHelper;
        private readonly RequestHelper requestHelper;
        public LoginController(CookieHelper cookieHelper, RequestHelper requestHelper)
        {
            this.cookieHelper = cookieHelper;
            this.requestHelper = requestHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            var request = requestHelper.Action<UserDto>($"user/{model.UserName}/{model.Password}", ActionType.Get, null);
            var result = request.Result.Data;
            if (result is not null)
            {
                string role = (result.Role == 1) ? "user" : "admin";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,model.UserName),
                    new Claim(ClaimTypes.Role,role)
                };
                cookieHelper.SignIn(claims, model.RememberMe, HttpContext, $"{role}_scheme");
                return RedirectToAction(nameof(Index), role);
            }
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
    }
}
