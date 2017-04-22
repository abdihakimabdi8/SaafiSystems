using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class OwnerRevenue
    {
        public int OwnerLoadID {get; set;}
        public OwnerLoad OwnerLoad { get; set; }

        public int RevenueID { get; set; }
        public Revenue Revenue { get; set; }
    }
}
