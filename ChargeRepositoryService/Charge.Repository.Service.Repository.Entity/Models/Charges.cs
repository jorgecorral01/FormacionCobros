using System;
using System.Collections.Generic;

namespace Charge.Repository.Service.Repository.Entity.Models
{
    public partial class Charges
    {
        public int IdCharge { get; set; }
        public string Concept { get; set; }
        public decimal Amount { get; set; }
        public string Identifier { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
