using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class EventAttendanceDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
