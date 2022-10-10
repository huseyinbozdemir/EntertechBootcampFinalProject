using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EventAttendanceController : ControllerBase
    {
        private IEventAttendanceService eventAttendanceService;
        private IEventService eventService;
        private IUserService userService;
        public EventAttendanceController(IEventAttendanceService eventAttendanceService, IEventService eventService, IUserService userService)
        {
            this.eventAttendanceService = eventAttendanceService;
            this.eventService = eventService;
            this.userService = userService;
        }

        [HttpGet]
        public BaseResponse<List<EventAttendanceDto>> GetAll(int include = 0)
            => include == 0
            ? new BaseResponse<List<EventAttendanceDto>>(eventAttendanceService.GetAll())
            : new BaseResponse<List<EventAttendanceDto>>(eventAttendanceService.GetAll(null, c => c.Event, c => c.User));

        [HttpGet("GetAttendeds/{id}")]
        public BaseResponse<List<EventAttendanceDto>> GetAttendeds(int id)
        {
            var data = eventAttendanceService.GetAll(ea => ea.UserId == id, ea => ea.Event);
            if (data is null)
                return new BaseResponse<List<EventAttendanceDto>>("Kullanıcı bulunamadı.");
            return new BaseResponse<List<EventAttendanceDto>>(data.Where(ea => ea.Event.EventDate < DateTime.Now).ToList());
        }
        [HttpGet("GetNextAttends/{id}")]
        public BaseResponse<List<EventAttendanceDto>> GetNextAttends(int id)
        {
            var data = eventAttendanceService.GetAll(ea => ea.UserId == id, ea => ea.Event);
            if (data is null)
                return new BaseResponse<List<EventAttendanceDto>>("Kullanıcı bulunamadı.");
            return new BaseResponse<List<EventAttendanceDto>>(data.Where(ea => ea.Event.EventDate >= DateTime.Now).ToList());
        }
        [HttpPost("{eventId}/{userId}")]
        public BaseResponse<EventAttendanceDto> Add(int eventId, int userId)
        {
            var @event = eventService.Get(e => e.EventId == eventId);
            var user = userService.Get(u => u.UserId == userId);
            if (@event is null)
                return new BaseResponse<EventAttendanceDto>("Etkinlik bulunamadı.");
            if (user is null)
                return new BaseResponse<EventAttendanceDto>("Kullanıcı bulunamadı");
            if (@event.Capacity < 1)
                return new BaseResponse<EventAttendanceDto>("Kapasite yetersiz.");
            @event.Capacity--;
            eventService.Update(@event);
            var eventAttendance = new EventAttendanceDto { EventId = eventId, UserId = userId };
            eventAttendanceService.Add(eventAttendance);
            return new BaseResponse<EventAttendanceDto>(eventAttendance);
        }
        [HttpDelete("{eventId}/{userId}")]
        public BaseResponse<EventAttendanceDto> Delete(int eventId, int userId)
        {
            var attendance = eventAttendanceService.Get(ea => ea.EventId == eventId && ea.UserId == userId);
            var @event = eventService.Get(e => e.EventId == eventId);
            var user = userService.Get(u => u.UserId == userId);
            if (@event is null)
                return new BaseResponse<EventAttendanceDto>("Etkinlik bulunamadı.");
            if (user is null)
                return new BaseResponse<EventAttendanceDto>("Kullanıcı bulunamadı");
            eventAttendanceService.Delete(attendance);
            @event.Capacity++;
            eventService.Update(@event);
            return new BaseResponse<EventAttendanceDto>(true);
        }

    }
}
