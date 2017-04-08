using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class DriverViewModel
    {
        public DateTime Date { get; set; }
        public string Refernce { get; set; }
        public string Description { get; set; }
        public string DriverName { get; set; }
        public string LoadMiles { get; set; }
        public string DeadMiles { get; set; }
        public string Rate { get; set; }
        public int DriverId { get; set; }
    }
}
