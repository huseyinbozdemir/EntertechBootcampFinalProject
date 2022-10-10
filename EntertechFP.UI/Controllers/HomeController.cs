using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Concrete;
using EntertechFP.EL.Concrete;
using EntertechFP.UI.Models;
using EntertechFP.UI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

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
            RequestHelper request = new RequestHelper();
            var key = configuration.GetSection("ApiKey").Get<string>();
            var user = new User { FirstName = "Huseyin", LastName = "Bozdemir", Password = "098f6bcd4621d373cade4e832627b4f6", EmailAddress = "test@gmail.com" };
            var result = request.Post<User>("user/1", key, user);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}