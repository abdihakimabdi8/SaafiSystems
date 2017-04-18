using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


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
        public IActionResult Index()
        {
            IEnumerable<RevenueCategory> revenueCategories = context.RevenueCategories.ToList();

            return View(revenueCategories);
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
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Categories";
            ViewBag.revenueCategories = context.RevenueCategories.ToList();
            return View();
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