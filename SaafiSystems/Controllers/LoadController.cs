using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SaafiSystems.Controllers
{
    public class LoadController : Controller
    {
        private readonly SaafiDbContext context;

        public LoadController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Load> loads = context.Loads.Include(c => c.LoadCategory).Include(c => c.OwnerLoads).ToList();

            return View(loads);
        }

        public IActionResult Add()
        {
            IList<LoadCategory> loadCategories = context.LoadCategories.ToList();
            AddLoadViewModel addLoadViewModel =
                new AddLoadViewModel(context.LoadCategories.ToList());
            return View(addLoadViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddLoadViewModel addLoadViewModel)
        {
            if (ModelState.IsValid)
            {
                LoadCategory newLoadCategory =
                    context.LoadCategories.Single(c => c.ID == addLoadViewModel.LoadCategoryID);
                // Add the new cheese to my existing cheeses
                Load newLoad= new Load
                {
            
                    Date = addLoadViewModel.Date,
                    Reference = addLoadViewModel.Reference,
                    Description = addLoadViewModel.Description,
                    Owner = addLoadViewModel.Owner,
                    LoadCategory = newLoadCategory
                };

                context.Loads.Add(newLoad);
                context.SaveChanges();

                return Redirect("/Load");
            }

            return View(addLoadViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Load(s)";
            ViewBag.expenses = context.Loads.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] loadIds)
        {
            foreach (int loadId in loadIds)
            {
                Load theLoads = context.Loads.Single(c => c.ID == loadId);
                context.Loads.Remove(theLoads);
            }

            context.SaveChanges();

            return Redirect("/Load");
        }
        [HttpPost]
        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                Redirect("/Load");
            }

            LoadCategory theCategory = context.LoadCategories
                .Include(cat => cat.Loads)
                .Single(cat => cat.ID == id);

            /* 
             * IList<Cheese> theCheese = Context.Cheeses
             * .Include(c => c.Category)
             * .Single(c => c.CategoryID ==id)
             .ToList();*/

            ViewBag.Title = "Expenses in Category" + theCategory.Name;
            return View("Index", theCategory.Loads);
        }

    }
}