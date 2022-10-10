using AutoMapper;
using EntertechFP.API.Models;
using EntertechFP.EL.Concrete;

namespace EntertechFP.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(d => d.EventId, o => o.MapFrom(s => s.EventId))
                .ForMember(d => d.EventName, o => o.MapFrom(s => s.EventName))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.EventDate, o => o.MapFrom(s => s.EventDate))
                .ForMember(d => d.LastAttendDate, o => o.MapFrom(s => s.LastAttendDate))
                .ForMember(d => d.Capacity, o => o.MapFrom(s => s.Capacity))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.Fare, o => o.MapFrom(s => s.Fare))
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(d => d.CityName, o => o.MapFrom(s => s.City.CityName));
        }
    }
}
