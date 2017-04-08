﻿using SaafiSystems.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddRevenueViewModel
    {
        private List<RevenueCategory> list;

        [Required]
        [Display]
        public DateTime Date { get; set; }

        [Required]
        [Display]
        public string Reference { get; set; }


        [Required]
        [Display]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "Revenue")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your Revenue a description")]
        public string Description { get; set; }


        [Required]
        [Display]
        public int Amount { get; set; }


        [Required(ErrorMessage = "Provide revenue Category")]
        [Display(Name = "Category")]
        public int RevenueCategoryID { get; set; }

        public RevenueCategory RevenueCategory { get; set; }

        public List<SelectListItem> RevenueCategories { get; set; }

        public AddRevenueViewModel()
        {

            RevenueCategories = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach (SelectListItem category in RevenueCategories)
            {
                RevenueCategories.Add(new SelectListItem
                {
                    Value = ((int)RevenueCategoryID).ToString(),
                    Text = Name.ToString()
                });
            }

        }

        public AddRevenueViewModel(List<RevenueCategory> list)
        {
            this.list = list;
        }
    }
}