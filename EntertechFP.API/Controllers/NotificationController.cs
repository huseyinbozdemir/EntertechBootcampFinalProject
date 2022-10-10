using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class NotificationController : ControllerBase
    {
        private INotificationService notificationService;
        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        [HttpGet("{id}")]
        public BaseResponse<List<Notification>> GetAll(int id)
            => new BaseResponse<List<Notification>>(notificationService.GetAll(n => n.UserId == id));

        [HttpPost]
        public BaseResponse<Notification> Add([FromBody] Notification notification)
        {
            notification.IsSeen = false;
            notificationService.Add(notification);
            return new BaseResponse<Notification>(notification);
        }
        [HttpPatch("{id}")]
        public BaseResponse<Notification> Update(int id)
        {
            var notification = notificationService.Get(n => n.NotificationId == id);
            if (notification is null)
                return new BaseResponse<Notification>("Bildirim bulunamadı.");
            notification.IsSeen = true;
            notificationService.Update(notification);
            return new BaseResponse<Notification>(notification);
        }

    }
}
