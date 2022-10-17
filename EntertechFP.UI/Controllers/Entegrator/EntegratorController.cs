using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Controllers.Entegrator
{
    [Authorize(AuthenticationSchemes = "entegrator_scheme")]
    public class EntegratorController : Controller
    {
        private readonly CookieHelper cookieHelper;
        private readonly RequestHelper requestHelper;
        private static EntegratorDto entegrator;
        public EntegratorController(CookieHelper cookieHelper, RequestHelper requestHelper)
        {
            this.cookieHelper = cookieHelper;
            this.requestHelper = requestHelper;
        }
        private void GetEntegrator()
        {
            string id = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals("Id"))?.Value;
            entegrator = requestHelper.Action<EntegratorDto>($"entegrator/{id}?include=1", ActionType.Get, null).Result.Data;
        }
        public IActionResult Index()
        {
            GetEntegrator();
            return View(entegrator);
        }

    }
}
