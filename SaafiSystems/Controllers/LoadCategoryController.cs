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
    public class LoadCategoryController : Controller
    {
        private readonly SaafiDbContext context;

        public LoadCategoryController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<LoadCategory> loadCategories = context.LoadCategories.ToList();

            return View(loadCategories);
        }

        public IActionResult Add()
        {
            AddLoadCategoryViewModel addLoadCategoryViewModel =
                new AddLoadCategoryViewModel();
            return View(addLoadCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddLoadCategoryViewModel addLoadCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                LoadCategory newLoadCategory = new LoadCategory
                {
                    Name = addLoadCategoryViewModel.Name,



                };

                context.LoadCategories.Add(newLoadCategory);
                context.SaveChanges();

                return Redirect("/LoadCategory");
            }

            return View(addLoadCategoryViewModel);
        }
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Categories";
            ViewBag.loadCategories = context.LoadCategories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] loadCategoryIds)
        {
            foreach (int loadCategoryId in loadCategoryIds)
            {
                LoadCategory theLoadCategory = context.LoadCategories.Single(c => c.ID == loadCategoryId);
                context.LoadCategories.Remove(theLoadCategory);
            }

            context.SaveChanges();

            return Redirect("/LoadCategory");
        }
    }
}