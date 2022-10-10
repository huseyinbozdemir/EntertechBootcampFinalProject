using EntertechFP.EL.Concrete;

namespace EntertechFP.API.Models.Entities
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime LastAttendDate { get; set; }
        public int Capacity { get; set; }
        public string Address { get; set; }
        public decimal Fare { get; set; }
        public string CategoryName { get; set; }
        public string CityName { get; set; }
    }
}
