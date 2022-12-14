using System.Text.Json.Serialization;

namespace EntertechFP.UI.Models.Entitities
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime LastAttendDate { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public string Address { get; set; }
        public int? UserId { get; set; }
        public int? CityId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Fare { get; set; }
        public bool IsTicketed { get; set; }
        public bool? IsApproved { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CategoryDto? Category { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CityDto? City { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UserDto? User { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntegratorEventDto> EntegratorEvents { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EventAttendanceDto> EventAttendances { get; set; }
    }
}
