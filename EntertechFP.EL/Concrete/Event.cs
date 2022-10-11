using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime LastAttendDate { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public string Address { get; set; } = null!;
        public int? UserId { get; set; }
        public int? CityId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Fare { get; set; }
        public bool IsTicketed { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Category? Category { get; set; }
        public virtual City? City { get; set; }
        public virtual User? User { get; set; }
    }
}
