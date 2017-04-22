using Microsoft.AspNetCore.Mvc.Rendering;
using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddOwnerExpenseItemViewModel
    {


            public Owner Owner { get; set; }
            public List<SelectListItem> Expenses { get; set; }

            public int OwnerID { get; set; }
            public int ExpenseID { get; set; }



            public AddOwnerExpenseItemViewModel() { }
            public AddOwnerExpenseItemViewModel(Owner owner, IEnumerable<Expense> expenses)
            {
                Expenses = new List<SelectListItem>();
                {
                    foreach (var expense in expenses)
                    {
                        Expenses.Add(new SelectListItem
                        {
                            Value = expense.ID.ToString(),
                            Text = expense.Reference,

                        });
                        Owner = owner;
                    }
                }
            }
        }
    }
