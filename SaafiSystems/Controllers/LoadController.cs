using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            IList<RevenueCategory> revenueCategories = context.RevenueCategories.ToList();
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
                
                Load newLoad= new Load
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


        //public async Task<IActionResult> Edit(int? id)
        //{
        //    AddLoadViewModel addLoadViewModel =
        //        new AddLoadViewModel(context.LoadCategories.ToList());
        //    IList<LoadCategory> loadCategories = context.LoadCategories.ToList();
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loads = await context.Loads
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(m => m.ID == id);
        //    if (loads == null)
        //    {
        //        return NotFound();
        //    }
        //    LoadCategory newLoadCategory =
        //            context.LoadCategories.Single(c => c.ID == addLoadViewModel.LoadCategoryID);
        //    return View(loads);
        //} 


        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPost(int? id)
        //{
        //    AddLoadViewModel addLoadViewModel =
        //        new AddLoadViewModel(context.LoadCategories.ToList());
        //    Load newLoad = new Load
        //    {

        //        Date = addLoadViewModel.Date,
        //        Reference = addLoadViewModel.Reference,
        //        Description = addLoadViewModel.Description,
        //        Owner = addLoadViewModel.Owner,
        //        Amount = addLoadViewModel.Amount,
        //        LoadCategory = newLoadCategory,

        //    };

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loadToUpdate = await context.Loads
        //        .SingleOrDefaultAsync(c => c.ID == id);

        //    if (await TryUpdateModelAsync<Load>(loadToUpdate,
        //        "",
        //        c => c.Date, c => c.Load, c => c.Title))
        //    {
        //        try
        //        {
        //            await context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    PopulateLoadCategoryDropDownList(loadToUpdate.LoadCategoryID);
        //    return View(loadToUpdate);
        //}
        //private void PopulateLoadCategoryDropDownList(object selectedCategory = null)
        //{
        //    var loadCategoryQuery = from d in context.LoadCategories
        //                           orderby d.Name
        //                           select d;
        //    ViewBag.DepartmentID = new SelectList(loadCategoryQuery.AsNoTracking(), "LoadCategoryID", "Name", selectedCategory);
        //}
    }
}