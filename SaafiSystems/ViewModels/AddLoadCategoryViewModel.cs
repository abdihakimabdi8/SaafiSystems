
using SaafiSystems.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddLoadCategoryViewModel
    {
        [Display(Name = "Load Category")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please provide a load catogory")]
        public string Name { get; set; }

        public List<Load> Loads { get; set; }

    }
}
