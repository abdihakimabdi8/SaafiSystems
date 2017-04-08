using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaafiSystems.Models;
using SaafiSystems.ViewModels;


namespace SaafiSystems.Models
{
    public class Driver
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
