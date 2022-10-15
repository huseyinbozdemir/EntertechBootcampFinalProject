using System.Text.Json.Serialization;

namespace EntertechFP.UI.Models.Entitities
{
    public class EventAttendanceDto
    {
        public int AttendanceId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EventDto Event { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UserDto User { get; set; }
    }
}
