using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime NotificationDate { get; set; }
        public bool IsSeen { get; set; }

        public virtual User? User { get; set; } = null!;
    }
}
