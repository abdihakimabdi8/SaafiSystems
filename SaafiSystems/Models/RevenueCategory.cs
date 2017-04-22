using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class RevenueCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<Revenue> Revenues { get; set; }
        public IList<Load> Loads { get; set; }

    }
}