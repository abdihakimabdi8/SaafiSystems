using SaafiSystems.Models;
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

        public int ID { get; set; }
        [Required]
        [Display]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Provide reference number")]
        [Display]
        public string Reference { get; set; }


        [Required]
        [Display]
        public string Owner { get; set; }

        [Required(ErrorMessage = "You must give your Revenue a description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Provide revenue Amount")]
        [Display]
        public int Amount { get; set; }


        [Required(ErrorMessage = "Provide revenue Category")]
        [Display(Name = "Category")]
        public int RevenueCategoryID { get; set; }

        public RevenueCategory RevenueCategory { get; set; }

        public List<SelectListItem> RevenueCategories { get; set; }

        public AddRevenueViewModel() { }
        public AddRevenueViewModel(IEnumerable<RevenueCategory> revenueCategories, Revenue revenue)
        {

            RevenueCategories = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach (RevenueCategory category in revenueCategories)
            {
                RevenueCategories.Add(new SelectListItem
                {
                    Value = ((int)category.ID).ToString(),
                    Text = category.Name.ToString()
                });
            }

        }
    }
}