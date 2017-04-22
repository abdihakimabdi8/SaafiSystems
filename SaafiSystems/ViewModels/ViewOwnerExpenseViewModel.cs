using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaafiSystems.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SaafiSystems.ViewModels
    {
        public class ViewOwnerExpenseViewModel
        {

            public IList<OwnerExpense> ExpenseItems { get; set; }
            public Owner Owner { get; set; }

            public int OwnerID { get; set; }
            //    public Owner Owners { get; set; }

            public List<SelectListItem> Owners { get; set; }
            public ViewOwnerExpenseViewModel() { }
            public ViewOwnerExpenseViewModel(IEnumerable<Owner> owners)
            {
                Owners = new List<SelectListItem>();
                foreach (var owner in owners)
                {
                    Owners.Add(new SelectListItem
                    {
                        Value = owner.ID.ToString(),
                        Text = owner.Name
                    });
                }
            }


        }
    }
