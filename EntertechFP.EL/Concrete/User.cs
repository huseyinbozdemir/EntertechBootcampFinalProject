using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class User
    {
        public User()
        {
            EventAttendances = new HashSet<EventAttendance>();
            Events = new HashSet<Event>();
            Notifications = new HashSet<Notification>();
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte Role { get; set; }

        public virtual ICollection<EventAttendance>? EventAttendances { get; set; }
        public virtual ICollection<Event>? Events { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
