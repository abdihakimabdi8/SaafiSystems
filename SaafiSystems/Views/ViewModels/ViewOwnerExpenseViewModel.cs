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
        public Owner Owner { get; set; }

        public ExpenseCategory ExpenseCategory { get; set; }
        public IList<OwnerExpense> ExpenseItems { get; set; }

        //TODO: #3 refactor similar code with ViewOwnerExpenseItemsViewModel


    }
}
