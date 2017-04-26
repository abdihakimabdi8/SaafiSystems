using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaafiSystems.Controllers
{
    public class RevenueCategoryController : Controller
    {
        private readonly SaafiDbContext context;

        public RevenueCategoryController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int? page)
        {
            var revenueCategories = from lCat in context.RevenueCategories
                                    select lCat;
            int pageSize = 3;
            return View(await PaginatedList<RevenueCategory>.CreateAsync(revenueCategories.AsNoTracking(), page ?? 1, pageSize));

        }
       
        public IActionResult Add()
        {
            AddRevenueCategoryViewModel addRevenueCategoryViewModel =
                new AddRevenueCategoryViewModel();
            return View(addRevenueCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddRevenueCategoryViewModel addRevenueCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                RevenueCategory newRevenueCategory = new RevenueCategory
                {
                    Name = addRevenueCategoryViewModel.Name,
                };

                context.RevenueCategories.Add(newRevenueCategory);
                context.SaveChanges();

                return Redirect("/RevenueCategory");
            }

            return View(addRevenueCategoryViewModel);
        }
        public IActionResult Remove(int ID)
        {
            var revCat = context.RevenueCategories.Single(e => e.ID == ID);
            if (revCat != null)
                context.RevenueCategories.Remove(revCat);

            context.SaveChanges();

            return Redirect("/Load");


        }

        [HttpPost]
        public IActionResult Remove(int[] revenueCategoryIds)
        {
            foreach (int revenueCategoryId in revenueCategoryIds)
            {
                RevenueCategory theRevenueCategory = context.RevenueCategories.Single(c => c.ID == revenueCategoryId);
                context.RevenueCategories.Remove(theRevenueCategory);
            }

            context.SaveChanges();

            return Redirect("/RevenueCategory");
        }
    }
}