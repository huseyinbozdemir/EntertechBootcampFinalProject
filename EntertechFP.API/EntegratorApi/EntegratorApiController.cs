using AutoMapper;
using EntertechFP.API.Models.Entities;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace EntertechFP.API.EntegratorApi
{
    [Route("entegrator/api")]
    [ApiController]
    public class EntegratorApiController : ControllerBase
    {
        private IEventService eventService;
        private IEntegratorService entegratorService;
        private IEntegratorEventService entegratorEventService;
        private IMapper mapper;
        public EntegratorApiController(IMapper mapper, IEventService eventService, IEntegratorService entegratorService, IEntegratorEventService entegratorEventService)
        {
            this.eventService = eventService;
            this.entegratorService = entegratorService;
            this.entegratorEventService = entegratorEventService;
            this.mapper = mapper;
        }

        private bool IsExists(string apiKey) => !(entegratorService.Get(e => e.ApiKey.Equals(apiKey)) is null);
        private string JsonSerialize<T>(T data) => JsonSerializer.Serialize(data);
        private string XmlSerialize<T>(T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("Response"));
            using (var stringWriter = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
                {
                    serializer.Serialize(writer, data);
                    return stringWriter.ToString();
                }
            }
        }
        [HttpGet("GetEvents")]
        public string GetEvents(string? apiKey, string? type = "json")
        {
            if (IsExists(apiKey))
            {
                var data = eventService.GetAll(x => x.IsTicketed && x.EventDate > DateTime.Now && x.IsApproved);
                var dto = mapper.Map<List<EventDto>>(data);
                return (type.Equals("xml"))
                    ? XmlSerialize(new BaseResponse<List<EventDto>>(dto))
                    : JsonSerialize(new BaseResponse<List<EventDto>>(dto));
            }
            else
                return (type.Equals("xml"))
                    ? XmlSerialize(new BaseResponse<List<EventDto>>("Api Key doğrulanamadı."))
                    : JsonSerialize(new BaseResponse<List<EventDto>>("Api Key doğrulanamadı."));
        }
        [HttpPost("CreateTicket")]
        public BaseResponse<EntegratorEvent> CreateTicket(string? apiKey, int? eventId)
        {
            if (IsExists(apiKey))
            {
                var entegrator = entegratorService.Get(e => e.ApiKey.Equals(apiKey));
                var @event = eventService.Get(e => e.EventId == eventId);
                if (@event is not null)
                {
                    if (@event.EventDate < DateTime.Now || !@event.IsTicketed || !@event.IsApproved)
                        return new BaseResponse<EntegratorEvent>("Etkinlik uygun değil");
                    entegratorEventService.Add(new EntegratorEvent { EntegratorId = entegrator.EntegratorId, EventId = @event.EventId });
                    return new BaseResponse<EntegratorEvent>(true);
                }
                else
                    return new BaseResponse<EntegratorEvent>("Etkinlik bilgileri doğrulanamadı.");
            }
            else
                return new BaseResponse<EntegratorEvent>("Api Key doğrulanamadı.");
        }
        [HttpPut("TicketSold")]
        public BaseResponse<Event> TicketSold(string? apiKey, int? eventId)
        {
            if (IsExists(apiKey))
            {
                var entegrator = entegratorService.Get(e => e.ApiKey.Equals(apiKey));
                var entegratorEvent = entegratorEventService.Get(e => e.EntegratorId == entegrator.EntegratorId && e.EventId == eventId);
                if (entegratorEvent is null)
                    return new BaseResponse<Event>("Bilet entegrasyonu bulunamadı.");
                var @event = eventService.Get(e => e.EventId == eventId);
                @event.Capacity--;
                eventService.Update(@event);
                return new BaseResponse<Event>(true);
            }
            else
                return new BaseResponse<Event>("Api Key doğrulanamadı.");
        }
        [HttpPut("CancelTicket")]
        public BaseResponse<Event> CancelTicket(string? apiKey, int? eventId)
        {
            if (IsExists(apiKey))
            {
                var entegrator = entegratorService.Get(e => e.ApiKey.Equals(apiKey));
                var entegratorEvent = entegratorEventService.Get(e => e.EntegratorId == entegrator.EntegratorId && e.EventId == eventId);
                if (entegratorEvent is null)
                    return new BaseResponse<Event>("Bilet entegrasyonu bulunamadı.");
                var @event = eventService.Get(e => e.EventId == eventId);
                @event.Capacity++;
                eventService.Update(@event);
                return new BaseResponse<Event>(true);
            }
            else
                return new BaseResponse<Event>("Api Key doğrulanamadı.");
        }
    }
}