using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class User
    {
        public User()
        {
            Events = new HashSet<Event>();
            Notifications = new HashSet<Notification>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
