namespace EntertechFP.UI.Models.Entitities
{
    public class EventAttendanceDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }

        public EventDto Event { get; set; }
        public UserDto User { get; set; }
    }
}
