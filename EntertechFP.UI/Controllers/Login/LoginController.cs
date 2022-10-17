using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils.Helpers;
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
            var cookies = HttpContext.Request.Cookies;
            //if (cookies.ContainsKey("user_session"))
            //    return RedirectToAction("Index", "User");
            //else if (cookies.ContainsKey("entegrator_session"))
            //    return RedirectToAction("Index", "Entegrator");
            //else if (cookies.ContainsKey("admin_session"))
            //    return RedirectToAction("Index", "Admin");
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!model.IsEntegrator)
            {
                var request = requestHelper.Action<UserDto>($"user/{model.UserName}/{model.Password}", ActionType.Get, null);
                var result = request.Result.Data;
                if (result is not null)
                {
                    string role = (result.Role == 1) ? "user" : "admin";
                    var claims = new List<Claim>
                    {
                        new Claim("Id",result.UserId.ToString()),
                        new Claim(ClaimTypes.Role,role)
                    };
                    cookieHelper.SignIn(claims, model.RememberMe, HttpContext, $"{role}_scheme");
                    return RedirectToAction(nameof(Index), role);
                }
            }
            else
            {
                var request = requestHelper.Action<EntegratorDto>($"entegrator/{model.UserName}/{model.Password}", ActionType.Get, null);
                var result = request.Result.Data;
                if (result is not null)
                {
                    string role = "entegrator";
                    var claims = new List<Claim>
                    {
                        new Claim("Id",result.EntegratorId.ToString()),
                        new Claim(ClaimTypes.Role,role)
                    };
                    cookieHelper.SignIn(claims, model.RememberMe, HttpContext, $"{role}_scheme");
                    return RedirectToAction(nameof(Index), role);
                }
            }
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
    }
}
