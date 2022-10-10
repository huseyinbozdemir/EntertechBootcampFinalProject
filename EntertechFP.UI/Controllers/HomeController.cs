using EntertechFP.EL.Concrete;
using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var key = configuration.GetSection("ApiKey").Get<string>();
            RequestHelper request = new RequestHelper(key);
            var user = new User { FirstName = "Huseyin", LastName = "test", Password = "098f6bcd4621d373cade4e832627b4f6", EmailAddress = "test@gmail.com" };
            var result = request.Action<List<CategoryDto>>("category", ActionType.Get, null);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}