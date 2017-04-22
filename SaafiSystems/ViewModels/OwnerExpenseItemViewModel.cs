using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class OwnerExpenseItemViewModel
    {

        public IList<OwnerExpense> ExpenseItems { get; set; }
        public Owner Owner { get; set; }
    }
}
