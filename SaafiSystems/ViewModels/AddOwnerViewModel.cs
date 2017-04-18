﻿
using SaafiSystems.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddOwnerViewModel
    {


        [Display(Name = "Owner Operator")]
        [Required(ErrorMessage = "Please provide an owner operator catogory")]
        public string Name { get; set; }

    }
}
