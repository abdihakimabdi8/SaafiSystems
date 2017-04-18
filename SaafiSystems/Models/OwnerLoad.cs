using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class OwnerLoad
    {
        public int LoadID { get; set; }
        public Load Load { get; set; }


        public int OwnerID { get; set; }
        public Owner Owner { get; set; }


    }
}
