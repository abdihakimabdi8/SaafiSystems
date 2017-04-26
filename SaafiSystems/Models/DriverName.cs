using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class DriverName
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<Driver> Drivers { get; set; }

        public static implicit operator List<object>(DriverName v)
        {
            throw new NotImplementedException();
        }
    }
}
