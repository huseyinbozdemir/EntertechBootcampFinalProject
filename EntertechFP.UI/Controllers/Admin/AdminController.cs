using EntertechFP.UI.Models.Entitities;
using EntertechFP.UI.Models.ViewModels;
using EntertechFP.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EntertechFP.UI.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult Index()
        {
            return RedirectToAction(nameof(PendingEvents));
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
        public IActionResult PendingEvents(bool? success, int? status)
        {
            var request = requestHelper.Action<List<EventDto>>("event/?include=1&pending=1", ActionType.Get, null);
            var model = request.Result.Data;
            if(success is not null && status is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Status = (status == 0)?"Etkinlik reddedildi.":"Etkinlik onaylandı.";
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EventDetails(int? eventId)
        {
            if (eventId is null)
                return RedirectToAction(nameof(Events));
            var request = requestHelper.Action<EventDto>($"event/{eventId}?include=1", ActionType.Get, null);
            var model = request.Result.Data;
            if (model is null)
                return RedirectToAction(nameof(Events));
            return View(model);
        }
        [HttpGet]
        public IActionResult AcceptEvent(int? eventId)
        {
            if (eventId is null)
                return RedirectToAction(nameof(PendingEvents));
            var request = requestHelper.Action<EventDto>($"event/accept/{eventId}", ActionType.Patch, null);
            var success = request.Result.Success;
            if (success)
            {
                NotificationDto dto = new NotificationDto
                {
                    Description = $"{request.Result.Data.EventName} isimli etkinliğiniz kabul edildi.",
                    IsSeen=false,
                    UserId=request.Result.Data.UserId.Value,
                    NotificationDate=DateTime.Now
                };
                var notificationRequest = requestHelper.Action<NotificationDto>("notification", ActionType.Post, dto);
            }
            return RedirectToAction(nameof(PendingEvents), "Admin", new { success = success, status=1 });
        }
        [HttpGet]
        public IActionResult RejectEvent(int? eventId)
        {
            if (eventId is null)
                return RedirectToAction(nameof(PendingEvents));
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
            return RedirectToAction(nameof(PendingEvents), "Admin", new { success = success, status = 0 });
        }
        [HttpGet]
        public IActionResult RemoveEvent(int? eventId)
        {
            if (eventId is null)
                return RedirectToAction(nameof(Events));
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
        public IActionResult RemoveUser(int? userId)
        {
            if (userId is null)
                return RedirectToAction(nameof(Users));
            var request = requestHelper.Action<UserDto>($"user/{userId}", ActionType.Delete, null);
            var success = request.Result.Success;
            return RedirectToAction(nameof(Users), "Admin", new { success = success });
        }
        [HttpGet]
        public IActionResult UserDetails(int? userId)
        {
            if (userId is null)
                return RedirectToAction(nameof(Users));
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
            var request = requestHelper.Action<List<EntegratorDto>>("entegrator",ActionType.Get, null);
            var result = request.Result.Data;
            if(success is not null)
            {
                ViewBag.Alert = success;
            }
            return View(result);
        }
        public IActionResult EntegratorDetails(int? entegratorId)
        {
            if (entegratorId is null)
                RedirectToAction(nameof(Entegrators));
            var request = requestHelper.Action<EntegratorDto>($"entegrator/{entegratorId}", ActionType.Get, null);
            var result = request.Result.Data;
            if (result is null)
                return RedirectToAction(nameof(Entegrators));
            return View(result);
        }
        public IActionResult RemoveEntegrator(int? entegratorId)
        {
            if (entegratorId is null)
                RedirectToAction(nameof(Entegrators));
            var request = requestHelper.Action<EntegratorDto>($"entegrator/{entegratorId}", ActionType.Delete, null);
            var result = request.Result.Success;
            return RedirectToAction(nameof(Entegrators), "Admin", new { success = result });
        }
        #endregion
    }
}
