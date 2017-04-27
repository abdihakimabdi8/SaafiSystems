using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaafiSystems.ViewModels
{
    public class OwnerItemViewModel
    {
        public LoadCategory LoadCategory { get; set; }

        public IList<OwnerLoad> Items { get; set; }
        public Owner Owner { get; set; }
    }
}
