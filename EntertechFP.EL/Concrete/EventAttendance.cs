namespace EntertechFP.EL.Concrete
{
    public partial class EventAttendance
    {
        public int AttendanceId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        public virtual Event? Event { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
