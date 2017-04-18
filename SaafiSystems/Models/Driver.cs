using System;

namespace SaafiSystems.Models
{
    public class Driver
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public int LoadMiles { get; set; }
        public int DeadMiles { get; set; }
        public int Rate { get; set; }
        


        public int DriverNameID { get; set; }
        public DriverName DriverName { get; set; }
    }
 }
