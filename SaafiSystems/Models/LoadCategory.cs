using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class LoadCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<Load> Loads { get; set; }


    }
}