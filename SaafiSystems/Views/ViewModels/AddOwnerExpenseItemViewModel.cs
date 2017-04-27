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
            public List<Expense> Expenses { get; set; }

            public int OwnerID { get; set; }
            public int ExpenseID { get; set; }



            public AddOwnerExpenseItemViewModel() { }
            public AddOwnerExpenseItemViewModel(Owner owner, IEnumerable<Expense> expenses)
            {
                Expenses = new List<Expense>();
                {
                    foreach (var expense in expenses)
                    {
                        Expenses.Add(expense);
                        Owner = owner;
                    }
                }
            }
        }
    }
