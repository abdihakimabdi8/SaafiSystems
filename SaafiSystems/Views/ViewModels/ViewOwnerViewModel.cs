using SaafiSystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaafiSystems.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SaafiSystems.ViewModels
{
    public class ViewOwnerViewModel
    {

        public Owner Owner { get; set; }

        public IList<OwnerLoad> Items { get; set; }


        public LoadCategory LoadCategory { get; set; }


    }
}
