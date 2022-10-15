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
        private static UserDto user;
        public UserController(RequestHelper requestHelper) => this.requestHelper = requestHelper;
        private void GetUser()
        {
            string id = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals("Id"))?.Value;
            user = requestHelper.Action<UserDto>($"user/{id}?include=1", ActionType.Get, null).Result.Data;
        }
        public IActionResult Index()
        {
            GetUser();
            return View();
        }

        public IActionResult Profile(bool? success)
        {
            if(success is not null)
            {
                ViewBag.Alert = success;
                ViewBag.Message = TempData["status"];
            }
            return View(user);
        }

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
            return RedirectToAction("Profile", "User", new { success = request.Result.Success });
        }
        #endregion

        #region Event Section
        public IActionResult Events()
        {
            var eventRequest = requestHelper.Action<List<EventDto>>("event?include=1&active=1", ActionType.Get, null);
            var eventModel = eventRequest.Result.Data;
            var attendanceRequest = requestHelper.Action<List<EventAttendanceDto>>($"eventAttendance/GetNextAttends/{user.UserId}",ActionType.Get, null);
            var attendanceModel = attendanceRequest.Result.Data;
            UserEventsViewModel viewModel = new UserEventsViewModel
            {
                Events = eventModel,
                NextAttends = attendanceModel.Select(x => x.Event.EventId).ToList()
            };
            return View(viewModel);
        }
        #endregion
    }
}
