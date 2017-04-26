using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SaafiSystems.Controllers
{
    public class RevenueController : Controller
    {
        private readonly SaafiDbContext context;

        public RevenueController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }
        public async Task<IActionResult> Index(int? page)
        {

            var revenues = from r in context.Revenues.Include(c => c.RevenueCategory)
                        select r;
            int pageSize = 3;
            return View(await PaginatedList<Revenue>.CreateAsync(revenues.AsNoTracking(), page ?? 1, pageSize));
        }
        // GET: /<controller>/
        

        public IActionResult Add()
        {
            AddRevenueViewModel addRevenueViewModel =
                new AddRevenueViewModel(context.RevenueCategories.ToList(), null);
            return View(addRevenueViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddRevenueViewModel addRevenueViewModel)
        {
            if (ModelState.IsValid)
            {
                RevenueCategory newRevenueCategory =
                    context.RevenueCategories.Single(c => c.ID == addRevenueViewModel.RevenueCategoryID);
                // Add the new cheese to my existing cheeses
                Revenue newRevenue = new Revenue
                {
                    Date = addRevenueViewModel.Date,
                    Reference = addRevenueViewModel.Reference,
                    Owner = addRevenueViewModel.Owner,
                    Description = addRevenueViewModel.Description,
                    Amount = addRevenueViewModel.Amount,
                    RevenueCategory = newRevenueCategory
                };

                context.Revenues.Add(newRevenue);
                context.SaveChanges();

                return Redirect("/Revenue");
            }

            return View(addRevenueViewModel);
        }

        public IActionResult Remove(int ID)
        {
            var rev = context.Revenues.Single(e => e.ID == ID);
            if (rev != null)
                context.Revenues.Remove(rev);

            context.SaveChanges();

            return Redirect("/Revenue");


        }

        [HttpPost]
        public IActionResult Remove(int[] revenueIds)
        {
            foreach (int revenueId in revenueIds)
            {
                Revenue theRevenue = context.Revenues.Single(c => c.ID == revenueId);
                context.Revenues.Remove(theRevenue);
            }

            context.SaveChanges();

            return Redirect("/Revenue");
        }
        [HttpPost]
        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                Redirect("/Category");
            }

            RevenueCategory theCategory = context.RevenueCategories
                .Include(cat => cat.Revenues)
                .Single(cat => cat.ID == id);

            /* 
             * IList<Cheese> theCheese = Context.Cheeses
             * .Include(c => c.Category)
             * .Single(c => c.CategoryID ==id)
             .ToList();*/

            ViewBag.Title = "cheeses in Category" + theCategory.Name;
            return View("Index", theCategory.Revenues);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var revenue = context.Revenues.SingleOrDefault((l) => l.ID == id);
            if (revenue == null)
            {
                return NotFound();
            }
            List<RevenueCategory> newRevenueCategory =
                    new List<RevenueCategory>() { context.RevenueCategories.Single(c => c.ID == revenue.RevenueCategoryID) };
            return View(new AddRevenueViewModel(newRevenueCategory, null));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AddRevenueViewModel addRevenueViewModel)
        {
            var revenue = context.Revenues.SingleOrDefault((l) => l.ID == addRevenueViewModel.ID);
            if (revenue == null)
            {
                return NotFound();
            }
            var newRevenueCategories = context.RevenueCategories;

            var viewModel = new AddRevenueViewModel(newRevenueCategories, revenue);

            var existingRevenueCategory = context.RevenueCategories.SingleOrDefault(c => c.ID == revenue.RevenueCategoryID);

            if (existingRevenueCategory != null)
            {
                viewModel.RevenueCategory = existingRevenueCategory;
                viewModel.RevenueCategoryID = existingRevenueCategory.ID;
            }

            return View(viewModel);
        }

    }
}