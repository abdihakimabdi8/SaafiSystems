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

                return Redirect("/");
            }

            return View(addRevenueCategoryViewModel);
        }
    }
}