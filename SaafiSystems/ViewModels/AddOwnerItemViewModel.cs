using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SaafiSystems.ViewModels
{
    public class AddOwnerItemViewModel
    {

        public Owner Owner { get; set; }
        public List<SelectListItem> Loads { get; set; }

        public int OwnerID { get; set; }
        public int LoadID { get; set; }



        public AddOwnerItemViewModel() { }
        public AddOwnerItemViewModel(Owner owner, IEnumerable<Load> loads)
        {
            Loads = new List<SelectListItem>();
            {
                foreach (var load in loads)
                {
                    Loads.Add(new SelectListItem
                    {
                        Value = load.ID.ToString(),
                        Text = load.Reference,

                    });
                    Owner = owner;
                }
            }
        }
    }
}
