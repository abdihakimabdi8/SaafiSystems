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
    public class LoadCategoryController : Controller
    {
        private readonly SaafiDbContext context;

        public LoadCategoryController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var loadCategories = from lCat in context.LoadCategories
                                    select lCat;
            int pageSize = 3;
            return View(await PaginatedList<LoadCategory>.CreateAsync(loadCategories.AsNoTracking(), page ?? 1, pageSize));

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
        public IActionResult Remove(int ID)
        {
            var loadCat = context.LoadCategories.Single(e => e.ID == ID);
            if (loadCat != null)
                context.LoadCategories.Remove(loadCat);

            context.SaveChanges();

            return Redirect("/LoadCategory");


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