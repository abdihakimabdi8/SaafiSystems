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
        public List<Load> Loads { get; set; }

        public int OwnerID { get; set; }
        public int LoadID { get; set; }

        public Load Load { get; set; }


        public AddOwnerItemViewModel() { }
        public AddOwnerItemViewModel(Owner owner, IEnumerable<Load> loads)
        {
           Loads = new List<Load>();
            {
                foreach (var load in loads)
                {
                    Loads.Add(load);

                    Owner = owner;
                }
            }
        }
    }
}
