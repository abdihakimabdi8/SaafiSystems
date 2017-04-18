﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddDriverLoadViewModel
    {
        private Driver driver;

        [Required(ErrorMessage = "Please enter valid date")]
        [Display]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Please provide load refernce")]
        [Display]
        public string Reference { get; set; }

        

        [Required(ErrorMessage = "You must give your load a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Provide the number of Loaded Miles")]
        [Display(Name = "Loaded Miles")]

        public int LoadMiles { get; set; }
        [Required(ErrorMessage = "Provide the number of Dead Miles")]
        [Display(Name = "Dead Miles")]
        public int DeadMiles { get; set; }

        [Required]
        [Display]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Provide Driver Name")]
        [Display(Name = "Driver Name")]
        public int DriverNameID { get; set; }

        public List<DriverName> DriverName { get; set; }

        public List<SelectListItem> DriverNames { get; set; }


        public AddDriverLoadViewModel() { }
        public AddDriverLoadViewModel(IEnumerable<DriverName> driverNames)
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

        public AddDriverLoadViewModel(Driver driver)
        {
            this.driver = driver;
        }
    }
}
