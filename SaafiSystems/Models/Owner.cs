using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.Models
{
    public class Owner
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Name { get; set; }

        public IList<OwnerLoad> OwnerLoads { get; set; }

        public IList<OwnerExpense> OwnerExpenses { get; set; }
    }
}
