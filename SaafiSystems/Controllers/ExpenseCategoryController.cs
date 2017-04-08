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
    public class ExpenseCategoryController : Controller
    {
        private readonly SaafiDbContext context;

        public ExpenseCategoryController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<ExpenseCategory> expenseCategories = context.ExpenseCategories.ToList();

            return View(expenseCategories);
        }

        public IActionResult Add()
        {
            AddExpenseCategoryViewModel addExpenseCategoryViewModel =
                new AddExpenseCategoryViewModel();
            return View(addExpenseCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddExpenseCategoryViewModel addExpenseCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                ExpenseCategory newExpenseCategory = new ExpenseCategory
                {
                    Name = addExpenseCategoryViewModel.Name,



                };

                context.ExpenseCategories.Add(newExpenseCategory);
                context.SaveChanges();

                return Redirect("/");
            }

            return View(addExpenseCategoryViewModel);
        }
    }
}