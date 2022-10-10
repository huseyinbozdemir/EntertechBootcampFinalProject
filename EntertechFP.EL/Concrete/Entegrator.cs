using System;
using System.Collections.Generic;

namespace EntertechFP.EL.Concrete
{
    public partial class Entegrator
    {
        public int EntegratorId { get; set; }
        public string EntegratorName { get; set; } = null!;
        public string DomainName { get; set; } = null!;
        public string EmailAdress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
    }
}
