using Microsoft.AspNetCore.Mvc.Rendering;
using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class ViewDriverViewModel
    {
        public IList<Driver> Drivers { get; set; }
        public DriverName DriverName { get; set; }

        public int DriverNameID { get; set; }
        public List<SelectListItem> DriverNames { get; set; }


        public ViewDriverViewModel() { }
        public ViewDriverViewModel(IEnumerable<DriverName> driverNames)
        {

            DriverNames = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach (DriverName category in driverNames)
            {
                DriverNames.Add(new SelectListItem
                {
                    Value = ((int)category.ID).ToString(),
                    Text = category.Name.ToString()
                });
            }
        }
    }
}
