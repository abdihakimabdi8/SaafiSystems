using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaafiSystems.Models;
using SaafiSystems.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SaafiSystems.Models
{
    public class Load

    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public string Reference { get; set; }
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Description { get; set; }
        public string Owner { get; set; }

      //  public int LoadMiles { get; set; }
        public int Amount { get; set; }

      
        //public decimal PerMile {
        //    set
        //    {
        //        PerMile = Amount / LoadMiles;
        //    }
        //}

        public int LoadCategoryID { get; set; }
        public LoadCategory LoadCategory { get; set; }

        public IList<OwnerLoad> OwnerLoads { get; set; }
    }
}
