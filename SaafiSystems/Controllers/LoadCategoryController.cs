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

                return Redirect("/");
            }

            return View(addLoadCategoryViewModel);
        }
    }
}