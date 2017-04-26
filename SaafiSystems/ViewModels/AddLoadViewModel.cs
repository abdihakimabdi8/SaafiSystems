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

        public int ID { get; set; }

        [Required(ErrorMessage = "You must give your Load a date entry")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "You must give your Load a reference")]
        public string Reference { get; set; }

        [Required(ErrorMessage = "You must give your Load a description")]

        public string Description { get; set; }

        [Display(Name = "Owner")]
        [Required(ErrorMessage = "You must give your Owner Operator a name")]
        public string Owner { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "You must give your Revenue amount for this trip")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Provide Load Category")]
        [Display(Name = "Category")]
        public int LoadCategoryID { get; set; }

        public LoadCategory LoadCategory { get; set; }

        public int OwnerID { get; set; }
      //  public Owner Owner { get; set; }

        public List<SelectListItem> Owners { get; set; }
        public List<SelectListItem> LoadCategories { get; set; }


        public AddLoadViewModel() { }
        public AddLoadViewModel(IEnumerable<LoadCategory> loadCategories, Load load)
        {
            if (load != null)
            {
                this.ID = load.ID;
                this.Description = load.Description;
                this.Amount = load.Amount;
                this.Reference = load.Reference;
                this.Date = load.Date ?? DateTime.MinValue;
                this.Owner = load.Owner;
            }


            LoadCategories = new List<SelectListItem>();
            Owners = new List<SelectListItem>();
            // <option value="0">Hard</option>
            foreach (LoadCategory category in loadCategories)
            {
                LoadCategories.Add(new SelectListItem
                {
                    Value = ((int)category.ID).ToString(),
                    Text = category.Name.ToString()
                });
            }
            //        foreach (Owner owner in owners)
            //        {
            //            Owners.Add(new SelectListItem
            //            {
            //                Value = ((int)owner.ID).ToString(),
            //                Text = owner.Name.ToString()
            //            });
            //        }

            //}

        }

    }
}