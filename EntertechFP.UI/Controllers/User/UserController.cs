using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils;
using EntertechFP.UI.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.UI.Controllers.User
{
    [Authorize(AuthenticationSchemes = "user_scheme")]
    public class UserController : Controller
    {
        private readonly RequestHelper requestHelper;
        private readonly CookieHelper cookieHelper;
        private static UserDto user;
        public UserController(RequestHelper requestHelper, CookieHelper cookieHelper)
        {
            this.requestHelper = requestHelper;
            this.cookieHelper = cookieHelper;
        }
        private void GetUser()
        {
            string id = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals("Id"))?.Value;
            user = requestHelper.Action<UserDto>($"user/{id}?include=1", ActionType.Get, null).Result.Data;
        }
        public IActionResult Index()
        {
            GetUser();
            var notifications = user.Notifications;
            notifications.Reverse();
            if (notifications.Where(n => !n.IsSeen).ToList().Count > 0)
            {
                var request = requestHelper.Action($"notification/{user.UserId}", ActionType.Patch, user.Notifications);
            }
            return View(notifications);
        }
        #region Profile & Logout Section
        public IActionResult Profile(bool? success)
        {
            if (success is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Message = TempData["status"];
            }
            return View(user);
        }
        public IActionResult Logout()
        {
            cookieHelper.SignOut(HttpContext, "user_scheme");
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Change Password Section
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(UserChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            if (!user.Password.Equals(Util.HashToMD5(model.OldPassword)))
            {
                ModelState.AddModelError(String.Empty, "Eski şifreniz hatalı.");
                return View(model);
            }
            user.Password = model.NewPassword;
            var request = requestHelper.Action($"user/{user.UserId}", ActionType.Put, user);
            GetUser();
            TempData["status"] = "Şifreniz başarıyla değişti.";
            return RedirectToAction(nameof(Profile), "User", new { success = request.Result.Success });
        }
        #endregion

        #region Event Section
        public IActionResult Events(bool? success, int? cityId, int? categoryId)
        {
            var eventRequest = requestHelper.Action<List<EventDto>>("event?include=1&active=1", ActionType.Get, null);
            var eventModel = eventRequest.Result.Data;
            if (success is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Message = TempData["Message"];
            }
            if (cityId != null)
                eventModel = eventModel.Where(e => e.CityId == cityId).ToList();
            if (categoryId != null)
                eventModel = eventModel.Where(e => e.CategoryId == categoryId).ToList();
            GetCategoriesAndCities();
            return View(eventModel);
        }

        public IActionResult EventDetails(int eventId, bool? success)
        {
            var viewModel = new UserEventDetailsViewModel();
            var eventRequest = requestHelper.Action<EventDto>($"event/{eventId}?include=1", ActionType.Get, null);
            viewModel.Event = eventRequest.Result.Data;
            if (viewModel.Event.IsApproved == true)
            {
                if (viewModel.Event.IsTicketed)
                {
                    var entegratorRequest = requestHelper.Action<List<EntegratorEventDto>>($"entegratorEvent/{eventId}?include=1", ActionType.Get, null);
                    var entegratorModel = entegratorRequest.Result.Data;
                    if (entegratorModel is not null)
                    {
                        viewModel.Entegrators = entegratorModel.Select(e => e.Entegrator).ToList();
                    }
                }
                viewModel.IsAttended = viewModel.Event.EventAttendances.Where(e => e.UserId == user.UserId).FirstOrDefault() is not null;
                if (success is not null)
                {
                    ViewBag.Alert = success;
                    ViewBag.Message = TempData["Message"];
                }
                ViewBag.CanChange = viewModel.Event.UserId == user.UserId && (viewModel.Event.LastAttendDate - DateTime.Now).TotalDays > 5;
                return View(viewModel);
            }
            return RedirectToAction(nameof(Events), "User");
        }
        public IActionResult CancelEvent(int eventId)
        {
            GetUser();
            if (!user.Events.Any(e => e.EventId == eventId))
                return RedirectToAction(nameof(Events), "User");
            var request = requestHelper.Action<EventDto>($"event/{eventId}", ActionType.Delete, null);
            var success = request.Result.Success;
            TempData["Message"] = (success) ? "Etkinlik başarıyla silindi" : "Etkinlik silinemedi";
            return RedirectToAction(nameof(Events), "User", new { success = success });
        }
        public IActionResult AttendEvent(int eventId)
        {
            var request = requestHelper.Action<EventAttendanceDto>($"eventAttendance/{eventId}/{user.UserId}", ActionType.Post, null);
            TempData["Message"] = (request.Result.Success) ? "Başarılı bir şekilde etkinliğe katıldınız." : "Etkinliğine katılamadınız.";
            GetUser();
            return RedirectToAction(nameof(EventDetails), "User", new { eventId = eventId, success = request.Result.Success });
        }
        public IActionResult LeaveEvent(int eventId)
        {
            var request = requestHelper.Action<EventAttendanceDto>($"eventAttendance/{eventId}/{user.UserId}", ActionType.Delete, null);
            TempData["Message"] = (request.Result.Success) ? "Başarılı bir şekilde etkinlikten ayrıldınız." : "Etkinlikten ayrılamadınız.";
            GetUser();
            return RedirectToAction(nameof(EventDetails), "User", new { eventId = eventId, success = request.Result.Success });
        }
        public IActionResult AttendedEvents()
        {
            var request = requestHelper.Action<List<EventDto>>($"eventAttendance/GetAttendeds/{user.UserId}", ActionType.Get, null);
            var model = request.Result.Data;
            return View(model);
        }

        public IActionResult NextAttends()
        {
            var request = requestHelper.Action<List<EventDto>>($"eventAttendance/GetNextAttends/{user.UserId}", ActionType.Get, null);
            var model = request.Result.Data;
            return View(model);
        }

        public IActionResult MyEvents()
        {
            var request = requestHelper.Action<List<EventDto>>($"event?include=1&userId={user.UserId}", ActionType.Get, null);
            var model = request.Result.Data;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateEvent(EventDto model)
        {
            var request = requestHelper.Action($"event/{model.EventId}", ActionType.Patch, model);
            var success = request.Result.Success;
            TempData["Message"] = (success) ? "Etkinlik başarılı bir şekilde güncellendi." : "Etkinlik güncellenemedi.";
            return RedirectToAction(nameof(EventDetails), "User", new { eventId = model.EventId, success = request.Result.Success });

        }
        [HttpGet]
        public IActionResult CreateEvent()
        {
            GetCategoriesAndCities();
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(UserAddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                GetCategoriesAndCities();
                ViewBag.Error = "Lütfen bütün alanları doğru şekilde giriniz.";
                return View(model);
            }
            var eventModel = new EventDto
            {
                Address = model.Address,
                Capacity = model.Capacity,
                CategoryId = model.CategoryId,
                UserId = user.UserId,
                CityId = model.CityId,
                Fare = (model.IsTicketed) ? model.Fare : 0,
                IsTicketed = model.IsTicketed,
                Description = model.Description,
                EventDate = model.EventDate,
                LastAttendDate = model.LastAttendDate,
                EventName = model.EventName
            };
            var request = requestHelper.Action("event", ActionType.Post, eventModel);
            var success = request.Result.Success;
            TempData["Message"] = (success) ? "Etkinliğiniz başarıyla oluşturuldu." : "Etkinlik oluşturulamadı";
            return RedirectToAction(nameof(Events), "User", new { success = success });
        }

        private void GetCategoriesAndCities()
        {
            var categoriesRequest = requestHelper.Action<List<CategoryDto>>("category", ActionType.Get, null);
            var categories = categoriesRequest.Result.Data;
            var citiesRequest = requestHelper.Action<List<CityDto>>("city", ActionType.Get, null);
            var cities = citiesRequest.Result.Data;
            ViewBag.Categories = categories;
            ViewBag.Cities = cities;
        }
        #endregion

    }
}
