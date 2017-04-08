using SaafiSystems.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class AddExpenseViewModel
    {
        private List<ExpenseCategory> list;

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
        [Display(Name = "Expense")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your Expense a description")]
        public string Description { get; set; }


        [Required]
        [Display]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Provide Expense Category")]
        [Display(Name = "Category")]
        public int ExpenseCategoryID { get; set; }

        public ExpenseCategory ExpenseCategory { get; set; }

        public List<SelectListItem> ExpenseCategories { get; set; }

        public AddExpenseViewModel()
        {

            ExpenseCategories = new List<SelectListItem>();

            // <option value="0">Hard</option>
            foreach (SelectListItem category in ExpenseCategories)
            {
                ExpenseCategories.Add(new SelectListItem
                {
                    Value = ((int)ExpenseCategoryID).ToString(),
                    Text = Name.ToString()
                });
            }

        }

        public AddExpenseViewModel(List<ExpenseCategory> list)
        {
            this.list = list;
        }
    }
}