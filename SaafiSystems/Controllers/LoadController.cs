using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using PagedList.Core;

using System.ComponentModel.DataAnnotations;
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
        public async Task< IActionResult> Index(int? page)
        {
            
            var loads = from l in context.Loads.Include(c => c.LoadCategory).Include(c => c.OwnerLoads)
            select l;
            int pageSize = 3;
            return View(await PaginatedList<Load>.CreateAsync(loads.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Add()
        {
            //TODO: #6Render Owner/Operator List along Load Categories 
            IList<LoadCategory> loadCategories = context.LoadCategories.ToList();

            AddLoadViewModel addLoadViewModel =
                new AddLoadViewModel(context.LoadCategories.ToList(), null);
            return View(addLoadViewModel);
        }


        [HttpPost]
        public IActionResult Add(AddLoadViewModel addLoadViewModel)
        {
            if (ModelState.IsValid)
            {
                LoadCategory newLoadCategory =
                    context.LoadCategories.Single(c => c.ID == addLoadViewModel.LoadCategoryID);

                Load newLoad = new Load
                {

                    Date = addLoadViewModel.Date,
                    Reference = addLoadViewModel.Reference,
                    Description = addLoadViewModel.Description,
                    Owner = addLoadViewModel.Owner,
                    Amount = addLoadViewModel.Amount,
                    LoadCategory = newLoadCategory,

                };



                context.Loads.Add(newLoad);
                context.SaveChanges();

                return Redirect("/Load");
            }

            return View(addLoadViewModel);
        }

        public IActionResult Remove(int ID)
        {
            var ld = context.Loads.Single(e => e.ID == ID);
            if (ld != null)
                context.Loads.Remove(ld);

            context.SaveChanges();

            return Redirect("/Load");


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

            ViewBag.Title = "Loads in Category" + theCategory.Name;
            return View("Index", theCategory.Loads);
        }
        [HttpPost]
        public IActionResult RevCategory(int id)
        {
            if (id == 0)
            {
                Redirect("/Load");
            }

            RevenueCategory theCategory = context.RevenueCategories
                .Include(cat => cat.Loads)
                .Single(cat => cat.ID == id);

            ViewBag.Title = "Loads in Revenu Category" + theCategory.Name;
            return View("Index", theCategory.Loads);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var load = context.Loads.SingleOrDefault((l) => l.ID == id);
            if (load == null)
            {
                return NotFound();
            }
            List<LoadCategory> newLoadCategory =
                    new List<LoadCategory>() { context.LoadCategories.Single(c => c.ID == load.LoadCategoryID) };
            return View(new AddLoadViewModel(newLoadCategory, load));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AddLoadViewModel addLoadViewModel)
        {
            //TODO: #5 Edit Action Not fulle working
            var load = context.Loads.SingleOrDefault((l) => l.ID == addLoadViewModel.ID);

            if (load == null)
            {
                return NotFound();
            }
            var newLoadCategories = context.LoadCategories;

            var viewModel = new AddLoadViewModel(newLoadCategories, load);

            var existingLoadCateogry = context.LoadCategories.SingleOrDefault(c => c.ID == load.LoadCategoryID);

            if (existingLoadCateogry != null)
            {
                viewModel.LoadCategory = existingLoadCateogry;
                viewModel.LoadCategoryID = existingLoadCateogry.ID;

            }
            
            {
                await context.SaveChangesAsync();
                Redirect("/Load");


            }
            return View(viewModel);

        }
    }                
}