using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class DriverLoadsViewModel
    {
        public IList<Driver> Drivers { get; set; }
        public DriverName DriverName { get; set; }
    }
}
