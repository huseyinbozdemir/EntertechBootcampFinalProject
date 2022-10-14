using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntertechFP.UI.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly CookieHelper cookieHelper;
        private readonly RequestHelper requestHelper;
        public LoginController(IConfiguration configuration, CookieHelper cookieHelper, RequestHelper requestHelper)
        {
            this.configuration = configuration;
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
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,model.UserName),
                    new Claim(ClaimTypes.Role,(result.Role==1)?"User":"Admin")
                };
                cookieHelper.SignIn(claims, model.RememberMe, this);
                return RedirectToAction(nameof(Index), (result.Role == 1) ? "User" : "Admin");
            }
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
    }
}
