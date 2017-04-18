using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaafiSystems.Models;
using SaafiSystems.ViewModels;

namespace SaafiSystems.Models
{
    public class Load
  
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string  Reference { get; set; }
        public string  Description { get; set; }
        public string Owner { get; set; }
       

        public int LoadCategoryID { get; set; }
        public LoadCategory LoadCategory { get; set; }

        public IList<OwnerLoad> OwnerLoads { get; set; }
    }
}
