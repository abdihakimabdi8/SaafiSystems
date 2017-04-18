using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddDriverNameViewModel
    {
        [Display(Name = "Driver Name")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please provide a driver name")]
        public string Name { get; set; }

        public List<Driver> Drivers { get; set; }
    }
}
