using SaafiSystems.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddLoadViewModel
    {
        private List<LoadCategory> list;

        [Required(ErrorMessage = "You must give your Load a date entry")]
        public DateTime Date { get; set; }
        

        [Required(ErrorMessage = "You must give your Load a reference")]
        public string Reference { get; set; }

        [Required(ErrorMessage = "You must give your Load a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must provide an owner-operator")]
        public string Owner { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Provide Load Category")]
        [Display(Name = "Category")]
        public int LoadCategoryID { get; set; }

        public LoadCategory LoadCategory { get; set; }

        public List<SelectListItem> LoadCategories { get; set; }

        public AddLoadViewModel()
        {

            LoadCategories = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach (SelectListItem category in LoadCategories)
            {
                LoadCategories.Add(new SelectListItem
                {
                    Value = ((int)LoadCategoryID).ToString(),
                    Text = Name.ToString()
                });
            }

        }

        public AddLoadViewModel(List<LoadCategory> list)
        {
            this.list = list;
        }
    }
}