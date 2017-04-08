using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SaafiSystems.Controllers
{
    public class RevenueController : Controller
    {
        private readonly SaafiDbContext context;

        public RevenueController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Revenue> revenues = context.Revenues.Include(c => c.RevenueCategory).ToList();

            return View(revenues);
        }

        public IActionResult Add()
        {
            AddRevenueViewModel addRevenueViewModel =
                new AddRevenueViewModel(context.RevenueCategories.ToList());
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
                    Name = addRevenueViewModel.Name,
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

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Revenue";
            ViewBag.cheeses = context.Revenues.ToList();
            return View();
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

            return Redirect("/");
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

    }
}