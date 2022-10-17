using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Controllers.Register
{
    public class RegisterController : Controller
    {
        private readonly RequestHelper requestHelper;
        public RegisterController(RequestHelper requestHelper)
        {
            this.requestHelper = requestHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserRegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new UserDto
            {
                EmailAddress=model.EmailAddress,
                Password=model.Password,
                FirstName=model.FirstName,
                LastName=model.LastName
            };
            var request = requestHelper.Action("user", ActionType.Post, user);
            if(request.Result.Success==false)
            {
                ModelState.AddModelError("", "Bu email adresi zaten mevcut");
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Entegrator()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entegrator(EntegratorRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entegrator = new EntegratorDto
            {
                EmailAdress = model.EmailAddress,
                Password = model.Password,
                EntegratorName = model.EntegratorName,
                DomainName = model.DomainName
            };
            var request = requestHelper.Action("entegrator", ActionType.Post, entegrator);
            if (request.Result.Success == false)
            {
                ModelState.AddModelError("", "Bu email adresi zaten mevcut");
                return View(model);
            }
            return RedirectToAction("Index", "Login");
        }

    }
}
