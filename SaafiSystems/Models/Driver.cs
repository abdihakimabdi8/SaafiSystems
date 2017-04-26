using System;
using System.ComponentModel.DataAnnotations;

namespace SaafiSystems.Models
{
    public class Driver
    {
        
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
 
        public string Reference { get; set; }
        public string Description { get; set; }
        public int LoadMiles { get; set; }
        public int DeadMiles { get; set; }
        public int Rate { get; set; }

        //public int TripPay
        //{
        //    get
        //    {
        //        return Rate * DeadMiles;
        //    }
        //}

        //public int TotoalPay => TripPay++;

     
        public int DriverNameID { get; set; }
        public DriverName DriverName { get; set; }
    }
 }
