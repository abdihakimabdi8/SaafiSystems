using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class Revenue
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
  
        public string Reference { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        
        public int RevenueCategoryID { get; set; }
        public RevenueCategory RevenueCategory { get; set; }

       
    }
}
