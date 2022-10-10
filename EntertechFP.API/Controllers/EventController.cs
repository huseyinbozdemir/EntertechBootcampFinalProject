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
        public BaseResponse<List<Event>> GetAll(int? categoryId = null, int? cityId = null, int include = 0)
            => include == 0
            ? new BaseResponse<List<Event>>(eventService.GetAll(c => c.CityId == cityId || c.CategoryId == c.CategoryId))
            : new BaseResponse<List<Event>>(eventService.GetAll(c => c.CityId == cityId || c.CategoryId == c.CategoryId, c => c.City, c => c.User, c => c.Category));
        [HttpGet("{id}")]
        public BaseResponse<Event> Get(int id)
        {
            var data = eventService.Get(e => e.EventId == id, e => e.Category, e => e.City, e => e.User);
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
        public BaseResponse<Event> DeleteEvent(int id)
        {
            var @event = Get(id).Data;
            var days = (@event.LastAttendDate - DateTime.Now).TotalDays;
            if (days >= 5)
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
        [HttpPatch("Cancel/{id}")]
        public BaseResponse<Event> Cancel(int id)
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
