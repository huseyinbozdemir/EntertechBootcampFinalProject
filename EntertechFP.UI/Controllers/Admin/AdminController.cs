using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Controllers.Admin
{
    [Authorize(AuthenticationSchemes = "admin_scheme")]
    public class AdminController : Controller
    {
        private readonly CookieHelper cookieHelper;
        private readonly RequestHelper requestHelper;
        public AdminController(CookieHelper cookieHelper, RequestHelper requestHelper)
        {
            this.cookieHelper = cookieHelper;
            this.requestHelper = requestHelper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(PendingEvents));
        }
        [HttpGet]
        public IActionResult Logout()
        {
            cookieHelper.SignOut(HttpContext, "admin_scheme");
            return RedirectToAction("Login", "Index");
        }
        #region Event Section
        [HttpGet]
        public IActionResult Events(bool? success)
        {
            var request = requestHelper.Action<List<EventDto>>("event/?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            if (success is not null)
                ViewBag.Alert = success;
            return View(model);
        }
        [HttpGet]
        public IActionResult PendingEvents(bool? success)
        {
            var request = requestHelper.Action<List<EventDto>>("event/?include=1&pending=1", ActionType.Get, null);
            var model = request.Result.Data;
            if (success is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Status = TempData["message"];
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EventDetails(int eventId)
        {
            var request = requestHelper.Action<EventDto>($"event/{eventId}?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            if (model is null)
                return RedirectToAction(nameof(Events));
            return View(model);
        }
        [HttpGet]
        public IActionResult AcceptEvent(int eventId)
        {
            var request = requestHelper.Action<EventDto>($"event/accept/{eventId}", ActionType.Patch, null);
            var success = request.Result.Success;
            if (success)
            {
                NotificationDto dto = new NotificationDto
                {
                    Description = $"{request.Result.Data.EventName} isimli etkinliğiniz kabul edildi.",
                    IsSeen = false,
                    UserId = request.Result.Data.UserId.Value,
                    NotificationDate = DateTime.Now
                };
                var notificationRequest = requestHelper.Action<NotificationDto>("notification", ActionType.Post, dto);
            }
            TempData["message"] = "Etkinlik onaylandı.";
            return RedirectToAction(nameof(PendingEvents), "Admin", new { success = success });
        }
        [HttpGet]
        public IActionResult RejectEvent(int eventId)
        {
            var request = requestHelper.Action<EventDto>($"event/reject/{eventId}", ActionType.Patch, null);
            var success = request.Result.Success;
            if (success)
            {
                NotificationDto dto = new NotificationDto
                {
                    Description = $"{request.Result.Data.EventName} isimli etkinliğiniz reddedildi.",
                    IsSeen = false,
                    UserId = request.Result.Data.UserId.Value,
                    NotificationDate = DateTime.Now
                };
                var notificationRequest = requestHelper.Action<NotificationDto>("notification", ActionType.Post, dto);
            }
            TempData["message"] = "Etkinlik reddedildi.";
            return RedirectToAction(nameof(PendingEvents), "Admin", new { success = success });
        }
        [HttpGet]
        public IActionResult RemoveEvent(int eventId)
        {
            var request = requestHelper.Action<EventDto>($"event/{eventId}?admin=1", ActionType.Delete, null);
            var success = request.Result.Success;
            return RedirectToAction(nameof(Events), "Admin", new { success = success });
        }
        #endregion

        #region User Section
        [HttpGet]
        public IActionResult Users(bool? success)
        {
            var request = requestHelper.Action<List<UserDto>>("user?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            if (success is not null)
                ViewBag.Alert = success;
            return View(model);
        }
        [HttpGet]
        public IActionResult RemoveUser(int userId)
        {
            var request = requestHelper.Action<UserDto>($"user/{userId}", ActionType.Delete, null);
            var success = request.Result.Success;
            return RedirectToAction(nameof(Users), "Admin", new { success = success });
        }
        [HttpGet]
        public IActionResult UserDetails(int userId)
        {
            var request = requestHelper.Action<UserDto>($"user/{userId}?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            if (model is null)
                return RedirectToAction(nameof(Users));
            return View(model);
        }
        #endregion

        #region Entegrator Section
        public IActionResult Entegrators(bool? success)
        {
            var request = requestHelper.Action<List<EntegratorDto>>("entegrator", ActionType.Get, null);
            var result = request.Result.Data;
            if (success is not null)
            {
                ViewBag.Alert = success;
            }
            return View(result);
        }
        public IActionResult EntegratorDetails(int entegratorId)
        {
            var request = requestHelper.Action<EntegratorDto>($"entegrator/{entegratorId}", ActionType.Get, null);
            var result = request.Result.Data;
            if (result is null)
                return RedirectToAction(nameof(Entegrators));
            return View(result);
        }
        public IActionResult RemoveEntegrator(int entegratorId)
        {
            var request = requestHelper.Action<EntegratorDto>($"entegrator/{entegratorId}", ActionType.Delete, null);
            var result = request.Result.Success;
            return RedirectToAction(nameof(Entegrators), "Admin", new { success = result });
        }
        #endregion

        #region Category Section
        [HttpGet]
        public IActionResult Categories(bool? success)
        {
            var request = requestHelper.Action<List<CategoryDto>>("category?include=1", ActionType.Get, null);
            var categories = request.Result.Data;
            if (success is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Message = TempData["message"];
            }
            return View(categories);
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryDto category)
        {
            var request = requestHelper.Action("category", ActionType.Post, category);
            var success = request.Result.Success;
            TempData["success"] = success;
            if (success)
                TempData["message"] = "Kategori başarıyla eklendi.";
            else
                TempData["message"] = "Kategori eklenemedi.";
            return RedirectToAction(nameof(Categories), "Admin", new { success = success });
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryDto category)
        {
            var request = requestHelper.Action($"category/{category.CategoryId}", ActionType.Patch, category);
            var success = request.Result.Success;
            TempData["success"] = success;
            if (success)
                TempData["message"] = "Kategori başarıyla güncellendi.";
            else
                TempData["message"] = "Kategori güncellenemedi.";
            return RedirectToAction(nameof(Categories), "Admin", new { success = success });
        }

        [HttpGet]
        public IActionResult RemoveCategory(int categoryId)
        {
            var request = requestHelper.Action<CategoryDto>($"category/{categoryId}", ActionType.Delete, null);
            var success = request.Result.Success;
            TempData["success"] = success;
            if (success)
                TempData["message"] = "Kategori başarıyla silindi.";
            else
                TempData["message"] = "Kategori silinemedi.";
            return RedirectToAction(nameof(Categories), "Admin", new { success = success });
        }

        [HttpGet]
        public IActionResult CategoryDetails(int categoryId)
        {
            var request = requestHelper.Action<CategoryDto>($"category/{categoryId}?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            return View(model);
        }
        #endregion

        #region City Section
        public IActionResult Cities(bool? success)
        {
            var request = requestHelper.Action<List<CityDto>>("city?include=1", ActionType.Get, null);
            var cities = request.Result.Data;
            if (success is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Message = TempData["message"];
            }
            return View(cities);
        }

        [HttpPost]
        public IActionResult AddCity(CityDto city)
        {
            var request = requestHelper.Action("city", ActionType.Post, city);
            var success = request.Result.Success;
            TempData["success"] = success;
            if (success)
                TempData["message"] = "Şehir başarıyla eklendi.";
            else
                TempData["message"] = "Şehir eklenemedi.";
            return RedirectToAction(nameof(Cities), "Admin", new { success = success });
        }

        [HttpPost]
        public IActionResult UpdateCity(CityDto city)
        {
            var request = requestHelper.Action($"city/{city.CityId}", ActionType.Patch, city);
            var success = request.Result.Success;
            TempData["success"] = success;
            if (success)
                TempData["message"] = "Şehir başarıyla güncellendi.";
            else
                TempData["message"] = "Şehir güncellenemedi.";
            return RedirectToAction(nameof(Cities), "Admin", new { success = success });
        }

        [HttpGet]
        public IActionResult RemoveCity(int cityId)
        {
            var request = requestHelper.Action<CityDto>($"city/{cityId}", ActionType.Delete, null);
            var success = request.Result.Success;
            TempData["success"] = success;
            if (success)
                TempData["message"] = "Şehir başarıyla silindi.";
            else
                TempData["message"] = "Şehir silinemedi.";
            return RedirectToAction(nameof(Cities), "Admin", new { success = success });
        }

        [HttpGet]
        public IActionResult CityDetails(int cityId)
        {
            var request = requestHelper.Action<CityDto>($"city/{cityId}?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            return View(model);
        }
        #endregion
    }
}
