using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
