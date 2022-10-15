using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EventController : ControllerBase
    {
        private IEventService eventService;
        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public BaseResponse<List<Event>> GetAll(int? categoryId = null, int? cityId = null, int include = 0, int pending = 0, int active = 0)
        {
            var eventList = (include == 0)
                 ? eventService.GetAll()
                 : eventService.GetAll(null, c => c.City, c => c.User, c => c.Category);
            if (categoryId is not null)
                eventList = eventList.Where(e => e.CategoryId == categoryId).ToList();
            if (cityId is not null)
                eventList = eventList.Where(e => e.CityId == cityId).ToList();
            if (pending != 0)
                eventList = eventList.Where(e => e.IsApproved is null).ToList();
            if (active != 0)
                eventList = eventList.Where(e => e.EventDate > DateTime.Now && e.IsApproved == true).ToList();
            return new BaseResponse<List<Event>>(eventList);

        }
        [HttpGet("{id}")]
        public BaseResponse<Event> Get(int id, int include = 0)
        {
            var data = (include == 0)
                ? eventService.Get(e => e.EventId == id)
                : eventService.Get(e => e.EventId == id, e => e.Category, e => e.City, e => e.User, e => e.EventAttendances);
            if (data is null)
                return new BaseResponse<Event>("Etkinlik bulunamadı.");
            return new BaseResponse<Event>(data);
        }
        [HttpPost]
        public BaseResponse<Event> Add([FromBody] Event e)
        {
            e.IsApproved = false;
            eventService.Add(e);
            return new BaseResponse<Event>(e);
        }
        [HttpPatch("{id}")]
        public BaseResponse<Event> UpdateEvent(int id, [FromBody] Event e)
        {
            var @event = Get(id).Data;
            var days = (@event.LastAttendDate - DateTime.Now).TotalDays;
            if (days >= 5)
            {
                @event.Address = e.Address;
                @event.Capacity = e.Capacity;
                eventService.Update(@event);
                return new BaseResponse<Event>(@event);
            }
            return new BaseResponse<Event>("Son katılım tarihine 5 günden az kaldığı için işlem gerçekleştirilemedi.");
        }
        [HttpDelete("{id}")]
        public BaseResponse<Event> DeleteEvent(int id, int admin = 0)
        {
            var @event = Get(id).Data;
            if (@event is null)
                return new BaseResponse<Event>("Etkinlik bulunamadı.");
            var days = (@event.LastAttendDate - DateTime.Now).TotalDays;
            if (days >= 5 || admin == 1)
            {
                eventService.Delete(@event);
                return new BaseResponse<Event>(true);
            }
            return new BaseResponse<Event>("Son katılım tarihine 5 günden az kaldığı için işlem gerçekleştirilemedi.");
        }
        [HttpPatch("Accept/{id}")]
        public BaseResponse<Event> Accept(int id)
        {
            var data = Get(id).Data;
            if (data is null)
                return new BaseResponse<Event>("Etkinlik bulunamadı");
            data.IsApproved = true;
            eventService.Update(data);
            return new BaseResponse<Event>(data);
        }
        [HttpPatch("Reject/{id}")]
        public BaseResponse<Event> Reject(int id)
        {
            var data = Get(id).Data;
            if (data is null)
                return new BaseResponse<Event>("Etkinlik bulunamadı");
            data.IsApproved = false;
            eventService.Update(data);
            return new BaseResponse<Event>(data);
        }
    }
}
