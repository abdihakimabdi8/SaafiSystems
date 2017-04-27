using System;
using System.Collections.Generic;
using System.Linq;
using SaafiSystems.Models;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Core;
using SaafiSystems.Models.MonthlyRevenueViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaafiSystems.Controllers
{
    public class SearchController : Controller
    {
        private readonly SaafiDbContext context;
        public SearchController(SaafiDbContext dbcontext)
        {
            context = dbcontext;
        }

        // GET: Reports
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";



            ViewData["OwnerSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Owner_desc" : "";
            ViewData["categorytortParm"] = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";

            ViewData["CurrentFilter"] = searchString;





            ViewBag.CurrentFilter = searchString;

            var loads = from l in context.Loads
                        select l;


            if (searchString != null)
            {

                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                loads = loads.Where(l => l.Date.ToString().Contains(searchString) || l.Reference.Contains(searchString) || l.Owner.ToUpper().Contains(searchString.ToUpper()) || l.Description.ToUpper().Contains(searchString.ToUpper()) || l.LoadCategory.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Owner_desc":
                    loads = loads.OrderByDescending(l => l.Owner);
                    break;
                case "Date":
                    loads = loads.OrderBy(l => l.Date);
                    break;
                case "date_desc":
                    loads = loads.OrderByDescending(l => l.Date);
                    break;
                default:
                    loads = loads.OrderBy(l => l.LoadCategory);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Load>.CreateAsync(loads.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Search(string searchTerm)
        {
            var loads = context.Loads.Where(c => c.Description.Contains(searchTerm)).ToList();
            return View(loads);

        }

        public async Task<ActionResult> MonthlyRevenue()
        {
            IQueryable<MonthlyRevenue> data =
                from load in context.Loads
                group load by load.Date into monthlyRevenueGroup
                select new MonthlyRevenue()
                {
                    Date = monthlyRevenueGroup.Key,
                    RevenueTotal = monthlyRevenueGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }


    }
}


