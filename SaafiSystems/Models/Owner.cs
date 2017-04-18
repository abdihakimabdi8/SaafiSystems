using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class Owner
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<OwnerLoad> OwnerLoads { get; set; }


    }
}
