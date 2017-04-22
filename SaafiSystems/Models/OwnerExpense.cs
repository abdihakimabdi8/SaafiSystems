using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class OwnerExpense
    {
        public int ExpenseID { get; set; }
        public Expense Expense { get; set; }


        public int OwnerID { get; set; }
        public Owner Owner { get; set; }
    }
}
