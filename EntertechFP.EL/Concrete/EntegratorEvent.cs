using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class EntegratorEvent
    {
        public int EntegrationId { get; set; }
        public int EntegratorId { get; set; }
        public int EventId { get; set; }

        public virtual Entegrator Entegrator { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;
    }
}
